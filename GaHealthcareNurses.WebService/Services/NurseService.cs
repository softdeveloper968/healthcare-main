using Contracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Common;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using Newtonsoft.Json;
using Repository;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PayPal.Api;
using AutoMapper;
using System.Linq;

namespace Services
{
    public class NurseService : INurseService
    {
        private INurseRepository _nurseRepository;
        private IEmployerService _employerService;
        private ICityService _cityService;
        private IStateService _stateService;
        private IAdminService _adminService;
        private HttpClient _httpClient;
        private IBuyCoursesService _buyCoursesService;
        private readonly IMapper _mapper;

        #region Constructor for NurseService
        public NurseService(INurseRepository nurseRepository, ICityService cityService, IStateService stateService, IAdminService adminService, HttpClient httpClient, IBuyCoursesService buyCoursesService, IEmployerService employerService, IMapper mapper)
        {
            _nurseRepository = nurseRepository;
            _cityService = cityService;
            _stateService = stateService;
            _adminService = adminService;
            _httpClient = httpClient;
            _buyCoursesService = buyCoursesService;
            _employerService = employerService;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<IEnumerable<Nurse>> GetAll()
        {
            return await _nurseRepository.GetAllNurses();
        }

        public async Task<Nurse> GetById(string id)
        {
            return await _nurseRepository.GetNurseById(id);
        }

        public async Task Add(Nurse nurse)
        {
            var city = await _cityService.GetById((int)nurse.CityId);
            var state = await _stateService.GetById((int)nurse.StateId);
            var latitudeAndLongitude = GetLatitudeAndLongitude.GetLatLogFromAddress(nurse.AddressLine1, city.Name, state.Name, city.ZipCode);
            nurse.Latitude = latitudeAndLongitude.Latitude;
            nurse.Longitude = latitudeAndLongitude.Longitude;
            await _nurseRepository.AddNurse(nurse);
        }

        public async Task Delete(Nurse nurse)
        {
            await _nurseRepository.DeleteNurse(nurse);
        }

        public async Task Update(Nurse nurse)
        {
            await _nurseRepository.UpdateNurse(nurse);
        }
        public async Task<Nurse> ChangeUserJobAvailability(JobAvailabilityViewModel jobAvailability)
        {
            var nurseData = await _nurseRepository.GetNurseById(jobAvailability.UserId);
            if (nurseData != null)
            {
                nurseData.IsUserAvailableForJob = jobAvailability.Status;
                await _nurseRepository.UpdateNurse(nurseData);
            }
            return nurseData;
        }

        public async Task<bool> ExecutePayment(string paymentId, string payerId)
        {
            if (!string.IsNullOrWhiteSpace(paymentId) && !string.IsNullOrWhiteSpace(payerId))
            {
                GetPaypalAccessToken paypalAccessToken = new GetPaypalAccessToken(_httpClient);

                var accessToken = await paypalAccessToken.GetAccessToken();

                ExecutePaymentRequestViewModel executePaymentRequestViewModel = new ExecutePaymentRequestViewModel
                {
                    payer_id = payerId
                };

                using (var client = new HttpClient())
                {
                    try
                    {
                        var myContent = JsonConvert.SerializeObject(executePaymentRequestViewModel);

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                        HttpContent requestObj = new StringContent(myContent, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/payments/payment/" + paymentId + "/execute", requestObj);

                        var responseString = await response.Content.ReadAsStringAsync();

                        var paymentResponse = JsonConvert.DeserializeObject<ExecutePaymentResponseViewModel>(responseString);

                        var courseExist = await _buyCoursesService.CourseExist(paymentResponse.Id);

                        if (paymentResponse != null && !courseExist)
                        {
                            BuyCourses courses = new BuyCourses
                            {
                                PaymentId = paymentResponse.Id,
                                Description = paymentResponse.Transactions[0].description,
                                PurchasedDate = paymentResponse.Update_Time,
                                NurseId = paymentResponse.Transactions[0].note_to_payee
                            };
                            await _buyCoursesService.AddPurchasedCourse(courses);

                            var nurse = await _nurseRepository.GetNurseById(courses.NurseId);

                            var admins = await _adminService.GetAllAdmins();

                            foreach (var admin in admins)
                            {
                                var adminData = await _adminService.GetAdminById(admin.Id);
                                string templatePath = Environment.CurrentDirectory + @"\\EmailTemplates\BuyCoursesAdminTemplate.xml";
                                await SendEmailToAdmin(adminData, nurse, templatePath, EmailTemplateType.BuyCourses.ToString());
                            }

                            string NurseTemplatePath = Environment.CurrentDirectory + @"\\EmailTemplates\BuyCoursesNurseTemplate.xml";
                            await SendEmailToNurse(nurse, NurseTemplatePath, EmailTemplateType.BuyCourses.ToString());
                        }

                        return response.IsSuccessStatusCode;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return false;
        }

        public async Task<string> BuyCourses(string nurseId)
        {
            var nurse = await _nurseRepository.GetNurseById(nurseId);
            if (nurse != null)
            {
                GetPaypalAccessToken paypalAccessToken = new GetPaypalAccessToken(_httpClient);
                var accessToken = await paypalAccessToken.GetAccessToken();
                BuyCoursesRequestViewModel buyCourses = new BuyCoursesRequestViewModel
                {
                    intent = "sale",
                    payer = new Payer
                    {
                        payment_method = "paypal",
                        payer_info = new PayerInfo
                        {
                            email = nurse.EmailAddress,
                            first_name = nurse.FirstName,
                            last_name = nurse.LastName
                        }
                    },
                    transactions = new List<TransactionViewModel> { },

                    redirect_urls = new RedirectUrls
                    {
                        return_url = "https://www.ushealthcarenurses.com/nurse/paymentsuccess",
                        cancel_url = "https://www.ushealthcarenurses.com/nurse/paymentfailed"
                    },
                };

                var transaction = new TransactionViewModel
                {
                    amount = new Amount
                    {
                        total = "55.00",
                        currency = "USD",
                        details = new Details
                        {
                            subtotal = "55.00"
                        }
                    },
                    description = "Buy Courses.",
                    note_to_payee = nurse.Id
                };

                buyCourses.transactions.Add(transaction);

                using (var client = new HttpClient())
                {
                    try
                    {
                        var myContent = JsonConvert.SerializeObject(buyCourses);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                        HttpContent requestObj = new StringContent(myContent, Encoding.UTF8, "application/json");
                        var buyCoursesResponse = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/payments/payment", requestObj);
                        var responseString = await buyCoursesResponse.Content.ReadAsStringAsync();
                        var deserializedResponse = (JsonConvert.DeserializeObject<BuyCoursesResponseViewModel>(responseString));
                        return deserializedResponse.Links[1].Href;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
            return null;
        }

        public async Task<IEnumerable<Nurse>> GetbyCreatedDate(int skip, int take, string filter)
        {
            return await _nurseRepository.GetAllNursesByCreatedDate(skip, take, filter);
        }

        public async Task<int> GetNursesCount(string filter)
        {
            return await _nurseRepository.GetAllNursesCount(filter);
        }

        public async Task<IEnumerable<NurseDetails>> GetNurses()
        {
            List<NurseDetails> nurseDetails = new List<NurseDetails>();
            var nurses = await _nurseRepository.GetNursesDetails();
            foreach (var detail in nurses)
            {
                var nurseInfo = new NurseDetails();
                nurseInfo.Longitude = Convert.ToDouble(detail.Longitude);
                nurseInfo.Latitude = Convert.ToDouble(detail.Latitude);
                nurseInfo.FirstName = detail.FirstName;
                nurseInfo.LastName = detail.LastName.Substring(0, 1);
                nurseInfo.StateName = detail.State.Name;
                nurseDetails.Add(nurseInfo);
            };
            return nurseDetails;
        }

        public async Task<bool> AddUpdateCnaSkills(NurseCnaSkillsViewModel nurseCnaSkills)
        {
            if (string.IsNullOrEmpty(nurseCnaSkills.NurseId))
                return false;
            var nurseData = await _nurseRepository.GetNurseById(nurseCnaSkills.NurseId);
            if (nurseData == null)
                return false;
            var nurseCnaSkillsData = await _nurseRepository.GetNurseCnaSkills(nurseCnaSkills.NurseId);
            if (nurseCnaSkillsData == null)
            {
                await _nurseRepository.AddNurseCnaSkills(_mapper.Map<NurseCnaSkills>(nurseCnaSkills));
            }
            else
            {
                nurseCnaSkillsData.Baths = nurseCnaSkills.Baths;
                nurseCnaSkillsData.Shampoo = nurseCnaSkills.Shampoo;
                nurseCnaSkillsData.NailCare = nurseCnaSkills.NailCare;
                nurseCnaSkillsData.OralCare = nurseCnaSkills.OralCare;
                nurseCnaSkillsData.RoutineSkinCare = nurseCnaSkills.RoutineSkinCare;
                nurseCnaSkillsData.Positioning = nurseCnaSkills.Positioning;
                nurseCnaSkillsData.BackRubs = nurseCnaSkills.BackRubs;
                nurseCnaSkillsData.AmbulanceAssistance = nurseCnaSkills.AmbulanceAssistance;
                nurseCnaSkillsData.OralAndNascalCare = nurseCnaSkills.OralAndNascalCare;
                nurseCnaSkillsData.RoutineCatheterCare = nurseCnaSkills.RoutineCatheterCare;
                nurseCnaSkillsData.TemperaturePulseAndRespirationMeasurements = nurseCnaSkills.TemperaturePulseAndRespirationMeasurements;
                nurseCnaSkillsData.BloodPressureMeasurements = nurseCnaSkills.BloodPressureMeasurements;
                nurseCnaSkillsData.MeasureHeightAndWeight = nurseCnaSkills.MeasureHeightAndWeight;
                nurseCnaSkillsData.AccurateMaintainIntakeAndOutputAndDocuments = nurseCnaSkills.AccurateMaintainIntakeAndOutputAndDocuments;
                nurseCnaSkillsData.DistinguishesMeasurementReportedTo = nurseCnaSkills.DistinguishesMeasurementReportedTo;
                nurseCnaSkillsData.AssistWithMealsAndSnacks = nurseCnaSkills.AssistWithMealsAndSnacks;
                nurseCnaSkillsData.FeedsPatientWithNormalSwallowingAbilities = nurseCnaSkills.FeedsPatientWithNormalSwallowingAbilities;
                nurseCnaSkillsData.FeedingTubesWithScopeOfCnaCertification = nurseCnaSkills.FeedingTubesWithScopeOfCnaCertification;
                nurseCnaSkillsData.AdministersEnemasInstructedBySupervisingNurse = nurseCnaSkills.AdministersEnemasInstructedBySupervisingNurse;
                nurseCnaSkillsData.AssistPatientPerformingTurnCoughAndDeepBreathingExercises = nurseCnaSkills.AssistPatientPerformingTurnCoughAndDeepBreathingExercises;
                nurseCnaSkillsData.PracticeOxygenSafety = nurseCnaSkills.PracticeOxygenSafety;
                nurseCnaSkillsData.ProperBodyMechanics = nurseCnaSkills.ProperBodyMechanics;
                nurseCnaSkillsData.TransferFromBedToChair = nurseCnaSkills.TransferFromBedToChair;
                nurseCnaSkillsData.TransferFromChairToBed = nurseCnaSkills.TransferFromChairToBed;
                nurseCnaSkillsData.ProperUseOfHoyerLifts = nurseCnaSkills.ProperUseOfHoyerLifts;
                nurseCnaSkillsData.ProperUseOfSpecialtyBeds = nurseCnaSkills.ProperUseOfSpecialtyBeds;
                nurseCnaSkillsData.Other = nurseCnaSkills.Other;
                nurseCnaSkillsData.AdaptCareToIncorporateNormalGrowthAndDevelopment = nurseCnaSkills.AdaptCareToIncorporateNormalGrowthAndDevelopment;
                nurseCnaSkillsData.AdaptMethodAndTerminologyOfPatients = nurseCnaSkills.AdaptMethodAndTerminologyOfPatients;
                nurseCnaSkillsData.EnsureSafeEnvironmentReflectingSpecificNeeds = nurseCnaSkills.EnsureSafeEnvironmentReflectingSpecificNeeds;
                nurseCnaSkillsData.HomeCare = nurseCnaSkills.HomeCare;
                nurseCnaSkillsData.NursingHome = nurseCnaSkills.NursingHome;
                nurseCnaSkillsData.AssistedLiving = nurseCnaSkills.AssistedLiving;
                nurseCnaSkillsData.Medical = nurseCnaSkills.Medical;
                nurseCnaSkillsData.Surgical = nurseCnaSkills.Surgical;
                nurseCnaSkillsData.Hospice = nurseCnaSkills.Hospice;
                nurseCnaSkillsData.Alzheimers = nurseCnaSkills.Alzheimers;
                nurseCnaSkillsData.Cancer = nurseCnaSkills.Cancer;
                nurseCnaSkillsData.Dementia = nurseCnaSkills.Dementia;
                nurseCnaSkillsData.Diabetes = nurseCnaSkills.Diabetes;
                nurseCnaSkillsData.Parkinsons = nurseCnaSkills.Parkinsons;
                nurseCnaSkillsData.CnaSignature = nurseCnaSkills.CnaSignature;
                nurseCnaSkillsData.Date = nurseCnaSkills.Date;
                await _nurseRepository.UpdateNurseCnaSkills(nurseCnaSkillsData);
            }
            return true;
        }

        public async Task<NurseCnaSkillsViewModel> GetCnaSkills(string nurseId)
        {
            return _mapper.Map<NurseCnaSkillsViewModel>(await _nurseRepository.GetNurseCnaSkills(nurseId));
        }

        public async Task<List<GetAllNursesResponseModel>> GetAllNursesIds()
        {
            return await _nurseRepository.GetAllNursesIds();
        }

        public async Task<bool> UpdateNurseStatus(UpdateNurseStatusViewModel model)
        {
            var nurse = await _nurseRepository.GetNurseById(model.NurseId);
            if (nurse == null)
                return false;
            nurse.IsInactive = model.IsInactive;
            await _nurseRepository.UpdateNurse(nurse);
            return true;
        }

        public async Task<NurseUpdateViewModel> GetNursePersonalInformation(string nurseId)
        {
            var nurse = await _nurseRepository.GetNurseById(nurseId);
            return _mapper.Map<NurseUpdateViewModel>(nurse);
        }

        public async Task<bool> UpdateNursePersonalInformation(NurseUpdateViewModel model)
        {
            var nurse = await _nurseRepository.GetNurseById(model.Id);
            if (nurse == null)
                return false;
            nurse.FirstName = model.FirstName;
            nurse.MiddleInitial = model.MiddleInitial;
            nurse.LastName = model.LastName;
            nurse.DateOfBirth = model.DateOfBirth;
            nurse.SocialSecurityNo = model.SocialSecurityNo;
            nurse.TelephoneNo = model.TelephoneNo;
            nurse.AddressLine1 = model.AddressLine1;
            nurse.StateId = model.StateId;
            nurse.CityId = model.CityId;
            nurse.ZipCode = model.ZipCode;
            if(nurse.Employers.Count > 0)
            {
                foreach(var employer in nurse.Employers)
                {
                    nurse.Employers.Remove(employer);
                }
            }
            if(model.Employers != null && model.Employers.Count > 0)
            {
                foreach(var employer in model.Employers)
                {
                    var employerData = await _employerService.GetById(employer);
                    nurse.Employers.Add(employerData);
                }
            }
            await _nurseRepository.UpdateNurse(nurse);
            return true;
        }
        #endregion

        #region Send Nurse Referral Email
        /// <summary>
        /// Add referrals records
        /// </summary>
        /// <param name="sendReferralViewModel"></param>
        /// <returns></returns>
        public async Task<string> AddReferral(SendReferralViewModel sendReferralViewModel)
        {
            try
            {
                //send mail if nurse has not registered already
                var nurse = await _nurseRepository.GetNurseById(sendReferralViewModel.NurseId);
                if (nurse.ReferralCount >= 25)
                {
                    var admins = await _adminService.GetAllAdmins();
                    foreach (var admin in admins)
                    {
                        var adminData = await _adminService.GetAdminById(admin.Id);
                        string templatePath = Environment.CurrentDirectory + @"\\EmailTemplates\ReferralAdminTemplate.xml";
                        var response = await this.SendEmailToAdmin(adminData, nurse, templatePath, EmailTemplateType.Referral.ToString());
                    }
                    string NurseTemplatePath = Environment.CurrentDirectory + @"\\EmailTemplates\ReferralNurseTemplate.xml";
                    var message = await this.SendEmailToNurse(nurse, NurseTemplatePath, EmailTemplateType.Referral.ToString());

                    return "Target of 25 referrals reached";
                }

                var totalRecords = sendReferralViewModel.Referrals.Count;
                var emailCount = 0;
                var emailAddresses = string.Empty;

                foreach (var referral in sendReferralViewModel.Referrals)
                {
                    var nurseEmail = await _nurseRepository.GetNurseByEmailId(referral.EmailAddress);
                    if (nurseEmail == null)
                    {
                        var referrralNurse = await _nurseRepository.GetReferralNursebyEmailId(referral.EmailAddress, sendReferralViewModel.NurseId);
                        if (referrralNurse == null)
                        {
                            emailCount++;
                        }
                        else
                            emailAddresses += referral.EmailAddress + ",";
                    }
                    else
                    {
                        emailAddresses += referral.EmailAddress + ",";
                    }
                }
                if (!string.IsNullOrEmpty(emailAddresses))
                {
                    emailAddresses = emailAddresses.TrimEnd(',');
                    emailAddresses = "Following email(s) already referred or registered: " + emailAddresses;
                }

                if (totalRecords == emailCount)
                {
                    foreach (var referral in sendReferralViewModel.Referrals)
                    {
                        var nurseEmail = await _nurseRepository.GetNurseByEmailId(referral.EmailAddress);
                        if (nurseEmail == null)
                        {
                            var referrralNurse = await _nurseRepository.GetReferralNursebyEmailId(referral.EmailAddress, sendReferralViewModel.NurseId);
                            if (referrralNurse == null)
                            {
                                Referral referrals = new Referral();
                                referrals.Name = referral.Name;
                                referrals.NurseId = sendReferralViewModel.NurseId;
                                referrals.EmailAddress = referral.EmailAddress;
                                referrals.ReferralLink = "https://www.ushealthcarenurses.com/nurse/register";
                                referrals.DateReferred = DateTime.Now;
                                referrals.EmailSent = true;
                                string templatePath = Environment.CurrentDirectory + @"\\EmailTemplates\NursesReferralTemplate.xml";
                                var refferalResponse = await _nurseRepository.AddReferral(referrals);
                                int refferalId = refferalResponse.ReferralId;
                                //update nurse table referral count
                                nurse.ReferralCount = nurse.ReferralCount == null ? 1 : nurse.ReferralCount + 1;
                                await _nurseRepository.UpdateNurse(nurse);
                                var message = await this.SendEmailToUser(referrals, refferalId, templatePath, EmailTemplateType.Referral.ToString());
                            }
                            emailAddresses = "Referral sent successfully!";
                        }

                    }

                }
                return emailAddresses;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> SendEmailToUser(Referral referralModel, int referalId, string templatePath, string type)
        {
            try
            {
                string emailBody = string.Empty;
                emailBody = Utility.GetEmailTemplateValue(templatePath, "NursesReferralEmail/Body");
                emailBody = emailBody.Replace("@@@username", referralModel.Name);
                emailBody = emailBody.Replace("@@@refferalMail", referralModel.EmailAddress);
                emailBody = emailBody.Replace("@@@referralId", Convert.ToString(referalId));

                Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.Refferal), referralModel.EmailAddress, emailBody);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SendEmailToAdmin(Admin admin, Nurse nurse, string templatePath, string type)
        {
            try
            {
                if (type == "Referral")
                {
                    string emailBody = string.Empty;
                    emailBody = Utility.GetEmailTemplateValue(templatePath, "ReferralAdminEmail/Body");
                    emailBody = emailBody.Replace("@@@nurse", nurse.FirstName + " " + nurse.LastName);
                    emailBody = emailBody.Replace("@@@adminName", admin.Name);
                    Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.Refferal), admin.Email, emailBody);
                }
                else
                {
                    string emailBody = string.Empty;
                    emailBody = Utility.GetEmailTemplateValue(templatePath, "BuyCoursesAdminEmail/Body");
                    emailBody = emailBody.Replace("@@@nurse", nurse.FirstName + " " + nurse.LastName);
                    emailBody = emailBody.Replace("@@@adminName", admin.Name);
                    Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.BuyCourses), admin.Email, emailBody);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SendEmailToNurse(Nurse nurse, string templatePath, string type)
        {
            try
            {
                if (type == "Referral")
                {
                    string emailBody = string.Empty;
                    emailBody = Utility.GetEmailTemplateValue(templatePath, "ReferralNurseEmail/Body");
                    emailBody = emailBody.Replace("@@@nursename", nurse.FirstName + " " + nurse.LastName);
                    Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.Refferal), nurse.EmailAddress, emailBody);
                }
                else
                {
                    string emailBody = string.Empty;
                    emailBody = Utility.GetEmailTemplateValue(templatePath, "BuyCoursesNurseEmail/Body");
                    emailBody = emailBody.Replace("@@@nursename", nurse.FirstName + " " + nurse.LastName);
                    Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.BuyCourses), nurse.EmailAddress, emailBody);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateReferral(Referral referral)
        {
            try
            {
                referral.DateRegistered = DateTime.Now;
                await _nurseRepository.UpdateReferral(referral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Referral> GetReferralById(int referralId)
        {
            var referral = await _nurseRepository.GetReferralById(referralId);
            return referral;

        }

        public async Task<string> ClaimReward(string nurseId)
        {
            var nurse = await _nurseRepository.GetNurseById(nurseId);
            if (nurse != null)
            {
                if (nurse.ReferralCount == 25)
                {
                    nurse.ReferralCount = 0;
                    nurse.RegisteredCount = 0;
                    nurse.TotalRewards = nurse.TotalRewards == null ? 1 : nurse.TotalRewards + 1;
                    await _nurseRepository.UpdateNurse(nurse);
                }
                return "Target not achieved yet. Please complete target of 25 referral!";
            }
            return "Nurse is null!";
        }
        #endregion
    }
}
