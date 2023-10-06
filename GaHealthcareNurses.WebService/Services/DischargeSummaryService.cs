using Contracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GaHealthcareNurses.Entity.Common;
using AutoMapper;
using Services.Helper;

namespace Services
{
    public class DischargeSummaryService : IDischargeSummaryService
    {
        private IDischargeSummaryRepository _dischargeSummaryRepository;
        private INurseService _nurseService;
        private IMapper _mapper;
        private IJobService _jobService;
        private IJobApplyService _jobApplyService;
        private IJobApplyForAgencyService _jobApplyForAgencyService;
        private IPushNotificationService _pushNotificationService;
        private INurseInboxService _nurseInboxService;

        #region Constructor for DischargeSummaryService
        public DischargeSummaryService(INurseService nurseService, IJobService jobService, IJobApplyService jobApplyService, IJobApplyForAgencyService jobApplyForAgencyService, IDischargeSummaryRepository dischargeSummaryRepository, IPushNotificationService pushNotificationService, INurseInboxService nurseInboxService, IMapper mapper)
        {
            _nurseService = nurseService;
            _jobService = jobService;
            _jobApplyService = jobApplyService;
            _jobApplyForAgencyService = jobApplyForAgencyService;
            _dischargeSummaryRepository = dischargeSummaryRepository;
            _pushNotificationService = pushNotificationService;
            _nurseInboxService = nurseInboxService;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> CreateDischargeSummary(DischargeSummary model)
        {
            return await _dischargeSummaryRepository.AddDischargeSummary(model);
        }

        public async Task<string> CompleteServiceRequestFromMobile(CompleteServiceRequestFromMobileRequestModel model)
        {
            var nurseJobRelation = await _jobApplyService.GetByJobIdAndNurseId(model.JobId, model.NurseId);
            if (nurseJobRelation == null)
                return "Nurse is not hired for this job";
            if (nurseJobRelation.Job.IsDischargeSummaryRequired)
            {
                var summary = await _dischargeSummaryRepository.GetDischargeSummaryByJobId(model.JobId);
                if (summary != null)
                    return "Discharge summary is already created for this service request";
                DischargeSummary dischargeSummary = new DischargeSummary
                {
                    JobId = model.JobId,
                    NurseId = model.NurseId,
                    EmployerId = nurseJobRelation.Job.EmployerId,
                    DateCreated = DateTime.UtcNow
                };
                await _dischargeSummaryRepository.AddDischargeSummary(dischargeSummary);
                return "You need to complete the discharge summary from web to complete this service request";
            }
            else
            {
                nurseJobRelation.StatusId = 7;
                await _jobApplyService.UpdateJobApply(nurseJobRelation);
                var hiredAgencyData = await _jobApplyForAgencyService.GetByJobIdAndStatusId(model.JobId, 13);
                if (hiredAgencyData != null && hiredAgencyData.ToList().Count > 0)
                {
                    hiredAgencyData.FirstOrDefault().StatusId = 14;
                    await _jobApplyForAgencyService.Update(hiredAgencyData.FirstOrDefault());
                }
                return "Service request is completed";
            }

        }

        public async Task<List<GetDischargeSummaryListResponseModel>> GetDischargeSummariesByNurseId(string nurseId)
        {
            var dischargeSummaries = await _dischargeSummaryRepository.GetDischargeSummariesByNurseId(nurseId);
            return _mapper.Map<List<GetDischargeSummaryListResponseModel>>(dischargeSummaries);
        }

        public async Task<List<GetDischargeSummaryListResponseModel>> GetDischargeSummariesByEmployerId(string employerId)
        {
            var dischargeSummaries = await _dischargeSummaryRepository.GetDischargeSummariesByEmployerId(employerId);
            return _mapper.Map<List<GetDischargeSummaryListResponseModel>>(dischargeSummaries);
        }

        public async Task<DischargeSummaryRequestModel> GetDischargeSummariesById(int id)
        {
            return _mapper.Map<DischargeSummaryRequestModel>(await _dischargeSummaryRepository.GetDischargeSummariesById(id));
        }

        public async Task<Response<DischargeSummaryPDFBytes>> GetDischargeSummaryPDFBytes(int id)
        {
            try
            {
                var dischargeSummary = _mapper.Map<DischargeSummaryRequestModel>(await _dischargeSummaryRepository.GetDischargeSummariesById(id));
                if (dischargeSummary == null)
                    return new Response<DischargeSummaryPDFBytes> { Status = "Error", Message = "Please enter valid discharge summary id" };
                var htmlString = Resource.DischargeSummaryPDF;
                #region parameters binding
                htmlString = htmlString.Replace("[[PatientName]]", dischargeSummary.PatientName);
                htmlString = htmlString.Replace("[[CurrentDate]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));
                htmlString = htmlString.Replace("[[DischargeChecked]]", GetCheckboxAttribute(dischargeSummary.Type, "Discharge"));
                htmlString = htmlString.Replace("[[TransferChecked]]", GetCheckboxAttribute(dischargeSummary.Type, "Transfer"));
                htmlString = htmlString.Replace("[[NoFurtherCareNeededChecked]]", GetCheckboxAttribute(dischargeSummary.NoFurtherCareNeeded));
                htmlString = htmlString.Replace("[[MovedOutOfServiceAreaChecked]]", GetCheckboxAttribute(dischargeSummary.MovedOutOfServiceArea));
                htmlString = htmlString.Replace("[[TransferredToAnotherAgencyChecked]]", GetCheckboxAttribute(dischargeSummary.TransferredToAnotherAgency));
                htmlString = htmlString.Replace("[[PhysicianRequestChecked]]", GetCheckboxAttribute(dischargeSummary.PhysicianRequest));
                htmlString = htmlString.Replace("[[FamilyAssumeResponsibilityChecked]]", GetCheckboxAttribute(dischargeSummary.FamilyAssumeResponsibility));
                htmlString = htmlString.Replace("[[AdmittedToHospitalChecked]]", GetCheckboxAttribute(dischargeSummary.AdmittedToHospital));
                htmlString = htmlString.Replace("[[DeathChecked]]", GetCheckboxAttribute(dischargeSummary.Death));
                htmlString = htmlString.Replace("[[PatientOrFamilyRefusedServicesChecked]]", GetCheckboxAttribute(dischargeSummary.PatientOrFamilyRefusedServices));
                htmlString = htmlString.Replace("[[AdmittedToSkilledNursingFacilityChecked]]", GetCheckboxAttribute(dischargeSummary.AdmittedToSkilledNursingFacility));
                htmlString = htmlString.Replace("[[AlertChecked]]", GetCheckboxAttribute(dischargeSummary.Alert));
                htmlString = htmlString.Replace("[[ForgetfulChecked]]", GetCheckboxAttribute(dischargeSummary.Forgetful));
                htmlString = htmlString.Replace("[[ConfusedChecked]]", GetCheckboxAttribute(dischargeSummary.Confused));
                htmlString = htmlString.Replace("[[OrientedChecked]]", GetCheckboxAttribute(dischargeSummary.Oriented));
                htmlString = htmlString.Replace("[[DisorientedChecked]]", GetCheckboxAttribute(dischargeSummary.Disoriented));
                htmlString = htmlString.Replace("[[DepressedChecked]]", GetCheckboxAttribute(dischargeSummary.Depressed));
                htmlString = htmlString.Replace("[[OtherChecked]]", GetCheckboxAttribute(dischargeSummary.Other));
                htmlString = htmlString.Replace("[[ComatoseChecked]]", GetCheckboxAttribute(dischargeSummary.Comatose));
                htmlString = htmlString.Replace("[[IndependentChecked]]", GetCheckboxAttribute(dischargeSummary.Independent));
                htmlString = htmlString.Replace("[[PartiallyDependentChecked]]", GetCheckboxAttribute(dischargeSummary.PartiallyDependent));
                htmlString = htmlString.Replace("[[TotallyDependentChecked]]", GetCheckboxAttribute(dischargeSummary.TotallyDependent));
                htmlString = htmlString.Replace("[[DiscussionWithPatientOrFamilyChecked]]", GetCheckboxAttribute(dischargeSummary.DiscussionWithPatientOrFamily));
                htmlString = htmlString.Replace("[[CompletedViaTelephoneChecked]]", GetCheckboxAttribute(dischargeSummary.CompletedViaTelephone));
                htmlString = htmlString.Replace("[[MutualAgreementForDischargeChecked]]", GetCheckboxAttribute(dischargeSummary.MutualAgreementForDischarge));
                htmlString = htmlString.Replace("[[Goals]]", string.IsNullOrEmpty(dischargeSummary.Goals) ? "" : dischargeSummary.Goals);
                htmlString = htmlString.Replace("[[Comments]]", string.IsNullOrEmpty(dischargeSummary.Comments) ? "" : dischargeSummary.Comments);
                htmlString = htmlString.Replace("[[Interventions]]", string.IsNullOrEmpty(dischargeSummary.Interventions) ? "" : dischargeSummary.Interventions);
                htmlString = htmlString.Replace("[[ResponseToInterventions]]", string.IsNullOrEmpty(dischargeSummary.ResponseToInterventions) ? "" : dischargeSummary.ResponseToInterventions);
                #endregion parameters binding 

                var bytes = HtmlToByte.ConvertToPDFBytes(htmlString);
                return new Response<DischargeSummaryPDFBytes> { Status = "Success", Data = new DischargeSummaryPDFBytes { Bytes = bytes } };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetCheckboxAttribute(string propertyValue, string compareBy)
        {
            return propertyValue == compareBy ? "checked" : "";
        }
        private string GetCheckboxAttribute(bool isChecked)
        {
            return isChecked ? "checked" : "";
        }

        public async Task<bool> CompleteDischargeSummary(DischargeSummaryRequestModel model)
        {
            var dischargeSummary = await _dischargeSummaryRepository.GetDischargeSummariesById(model.Id);
            if (dischargeSummary == null)
                return false;
            dischargeSummary.Type = model.Type;
            dischargeSummary.NoFurtherCareNeeded = model.NoFurtherCareNeeded;
            dischargeSummary.MovedOutOfServiceArea = model.MovedOutOfServiceArea;
            dischargeSummary.TransferredToAnotherAgency = model.TransferredToAnotherAgency;
            dischargeSummary.PhysicianRequest = model.PhysicianRequest;
            dischargeSummary.FamilyAssumeResponsibility = model.FamilyAssumeResponsibility;
            dischargeSummary.AdmittedToHospital = model.AdmittedToHospital;
            dischargeSummary.Death = model.Death;
            dischargeSummary.OtherReason = model.OtherReason;
            dischargeSummary.PatientOrFamilyRefusedServices = model.PatientOrFamilyRefusedServices;
            dischargeSummary.AdmittedToSkilledNursingFacility = model.AdmittedToSkilledNursingFacility;
            dischargeSummary.Alert = model.Alert;
            dischargeSummary.Forgetful = model.Forgetful;
            dischargeSummary.Confused = model.Confused;
            dischargeSummary.Agitated = model.Agitated;
            dischargeSummary.Oriented = model.Oriented;
            dischargeSummary.Disoriented = model.Disoriented;
            dischargeSummary.Depressed = model.Depressed;
            dischargeSummary.Other = model.Other;
            dischargeSummary.Comatose = model.Comatose;
            dischargeSummary.Independent = model.Independent;
            dischargeSummary.PartiallyDependent = model.PartiallyDependent;
            dischargeSummary.TotallyDependent = model.TotallyDependent;
            dischargeSummary.DiscussionWithPatientOrFamily = model.DiscussionWithPatientOrFamily;
            dischargeSummary.CompletedViaTelephone = model.CompletedViaTelephone;
            dischargeSummary.MutualAgreementForDischarge = model.MutualAgreementForDischarge;
            dischargeSummary.Goals = model.Goals;
            dischargeSummary.Comments = model.Comments;
            dischargeSummary.Interventions = model.Interventions;
            dischargeSummary.ResponseToInterventions = model.ResponseToInterventions;
            if (dischargeSummary.DateCompleted == null)
                dischargeSummary.DateCompleted = DateTime.UtcNow;
            await _dischargeSummaryRepository.UpdateDischargeSummary(dischargeSummary);
            return true;
        }

        public async Task<string> AcceptOrRejectDischargeSummary(AcceptOrRejectDischargeSummaryRequestModel model)
        {
            var dischargeSummary = await _dischargeSummaryRepository.GetDischargeSummariesById(model.Id);
            if (dischargeSummary == null)
                return "Enter valid discharge summary id";

            if (model.Status == (int)DischargeSummaryStatus.Verified)
            {
                dischargeSummary.Status = model.Status;
                dischargeSummary.Job.CareRecipient.Status = CareRecipientStatus.Discharged.ToString();
                dischargeSummary.Job.CareRecipient.Visibility = CareRecipientVisibility.Inactive.ToString();
                await _dischargeSummaryRepository.UpdateDischargeSummary(dischargeSummary);
                NurseInbox message = new NurseInbox
                {
                    NurseId = dischargeSummary.NurseId,
                    EmployerId = dischargeSummary.EmployerId,
                    JobId = dischargeSummary.JobId,
                    Subject = EnumHelper.GetDescription(NurseInboxSubject.DischargeSummaryAccepted),
                    SentDate = DateTime.UtcNow,
                    Message = $"Congratulations! Your Discharge Summary for the service request ({dischargeSummary.Job.JobTitle.Title}) for Care Recipient - {dischargeSummary.Job.CareRecipient.FirstName} {dischargeSummary.Job.CareRecipient.LastName} has been accepted."
                };
                await _nurseInboxService.AddMessage(message);
                var nurseData = await _nurseService.GetById(dischargeSummary.NurseId);
                Utility.SendMailToUser(message.Subject, nurseData.EmailAddress, message.Message);
                //var nurseJobRelation = await _jobApplyService.GetByJobIdAndNurseId(dischargeSummary.JobId, dischargeSummary.NurseId);
                //if (nurseJobRelation != null)
                //{
                //    nurseJobRelation.StatusId = 7;
                //    await _jobApplyService.UpdateJobApply(nurseJobRelation);
                //}
                //var hiredAgencyData = await _jobApplyForAgencyService.GetByJobIdAndStatusId(dischargeSummary.JobId, 13);
                //if (hiredAgencyData != null && hiredAgencyData.ToList().Count > 0)
                //{
                //    hiredAgencyData.FirstOrDefault().StatusId = 14;
                //    await _jobApplyForAgencyService.Update(hiredAgencyData.FirstOrDefault());
                //}
                return "Discharge summary is verified";
            }
            else
            {
                dischargeSummary.Status = model.Status;
                dischargeSummary.RejectionReason = model.RejectionReason;
                await _dischargeSummaryRepository.UpdateDischargeSummary(dischargeSummary);
                var dataObj = new
                {
                    title = "Discharge Summary Reject",
                    body = model.RejectionReason,
                    sound = "default",
                    messageType = "Discharge Summary Rejection"
                };
                var dataWrapper = new { data = dataObj };
                var nurse = await _nurseService.GetById(dischargeSummary.NurseId);
                if (!string.IsNullOrEmpty(nurse.FirebaseToken) && nurse.IsUserAvailableForJob)
                {
                    await _pushNotificationService.SendNotification(new
                    {
                        priority = "high",
                        to = nurse.FirebaseToken,
                        notification = dataObj
                    });
                }
                NurseInbox message = new NurseInbox
                {
                    NurseId = dischargeSummary.NurseId,
                    EmployerId = dischargeSummary.EmployerId,
                    JobId = dischargeSummary.JobId,
                    Subject = EnumHelper.GetDescription(NurseInboxSubject.DischargeSummaryRejected),
                    SentDate = DateTime.UtcNow,
                    Message = $"Unfortunately, your Discharge Summary for the service request ({dischargeSummary.Job.JobTitle.Title}) for Care Recipient - {dischargeSummary.Job.CareRecipient.FirstName} {dischargeSummary.Job.CareRecipient.LastName} has been rejected. Please update the Discharge Summary and forward to the Agency."
                };
                await _nurseInboxService.AddMessage(message);
                var nurseData = await _nurseService.GetById(dischargeSummary.NurseId);
                Utility.SendMailToUser(message.Subject, nurseData.EmailAddress, message.Message);
                return "Discharge summary is rejected";
            }
        }

        public async Task<bool> DeleteDischargeSummary(int id)
        {
            var dischargeSummary = await _dischargeSummaryRepository.GetDischargeSummariesById(id);
            if (dischargeSummary == null)
                return false;
            return await _dischargeSummaryRepository.DeleteDischargeSummary(dischargeSummary);
        }

        public async Task<List<GetDischargeSummaryListResponseModel>> GetAllDischargeSummaries()
        {
            var dischargeSummaries = await _dischargeSummaryRepository.GetAllDischargeSummaries();
            return _mapper.Map<List<GetDischargeSummaryListResponseModel>>(dischargeSummaries);
        }

        public async Task<bool> AssignDischargeSummary(AssignDischargeSummaryViewModel model)
        {
            var dischargeSummary = await _dischargeSummaryRepository.GetDischargeSummariesById(model.Id);
            if (dischargeSummary == null)
                return false;
            dischargeSummary.NurseId = model.NurseId;
            await _dischargeSummaryRepository.UpdateDischargeSummary(dischargeSummary);
            NurseInbox message = new NurseInbox
            {
                NurseId = model.NurseId,
                EmployerId = dischargeSummary.EmployerId,
                JobId = dischargeSummary.JobId,
                Subject = EnumHelper.GetDescription(NurseInboxSubject.DischargeSummaryCreated),
                SentDate = DateTime.UtcNow,
                Message = $"The Discharge Summary for the service request ({dischargeSummary.Job.JobTitle.Title}) for Care Recipient - {dischargeSummary.Job.CareRecipient.FirstName} {dischargeSummary.Job.CareRecipient.LastName} has been generated." +
                $"Please complete the Discharge Summary and forward to the Agency."
            };
            await _nurseInboxService.AddMessage(message);
            var nurseData = await _nurseService.GetById(model.NurseId);
            Utility.SendMailToUser(message.Subject, nurseData.EmailAddress, message.Message);
            return true;
        }
        #endregion
    }
}
