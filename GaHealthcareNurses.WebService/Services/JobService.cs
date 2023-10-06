using AutoMapper;
using Contracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Common;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using GoogleMaps.LocationServices;
using MoreLinq;
using Newtonsoft.Json;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class JobService : IJobService
    {
        private IJobRepository _jobRepository;
        private ICareRecipientService _careRecipientService;
        private ICityService _cityService;
        private IStateService _stateService;
        private IAdminService _adminService;
        private IAgencyServedCountiesService _agencyServedCountiesService;
        private IJobTitleService _jobTitleService;
        private IMapper _mapper;

        #region Constructor for JobService
        public JobService(IJobRepository jobRepository, IAdminService adminService, ICareRecipientService careRecipientService, IAgencyServedCountiesService agencyServedCountiesService, IJobTitleService jobTitleService, ICityService cityService, IStateService stateService, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _adminService = adminService;
            _careRecipientService = careRecipientService;
            _agencyServedCountiesService = agencyServedCountiesService;
            _mapper = mapper;
            _jobTitleService = jobTitleService;
            _stateService = stateService;
            _cityService = cityService;
        }
        #endregion

        #region Implementing Interface
        public async Task<IEnumerable<Job>> Get(PaginationFilter paginationFilter)
        {
            return await _jobRepository.Get(paginationFilter);
        }

        public async Task<int> TotalCount(string filter)
        {
            return await _jobRepository.TotalCount(filter);
        }

        //public async Task<GetJobsForAgencyViewModel> GetJobsForAgency(PaginationFilter paginationFilter)
        //{
        //    return await _jobRepository.GetJobsForAgency(paginationFilter);
        //}

        public async Task<IEnumerable<Job>> GetJobsForAgency(int skip, int take, string filter)
        {
            return await _jobRepository.GetJobsForAgency(skip, take, filter);
        }

        public async Task<Job> GetById(int id)
        {
            return await _jobRepository.GetById(id);
        }

        public async Task<IEnumerable<Job>> GetByClientId(string id)
        {
            return await _jobRepository.GetByClientId(id);
        }

        public async Task<JobApplyForAgency> GetByJobAndStatusId(int jobId, int statusId)
        {
            return await _jobRepository.GetByJobAndStatusId(jobId, statusId);
        }

        public async Task<Job> Add(Job job)
        {
            job.StatusId = 5;
            await _jobRepository.Add(job);

            var jobData = await GetById(job.JobId);
            var admins = await _adminService.GetAllAdmins();

            //Getting List Of Served Agencies 
            List<Employer> servedAgencies = new List<Employer>();
            string servedAgenciesText = string.Empty;
            string selectAgenciesForBidText = string.Empty;
            if (job.CareRecipient.City.CountyId != null)
            {
                var counties = await _agencyServedCountiesService.GetServedCountiesByCountyId(Convert.ToInt32(job.CareRecipient.City.CountyId));
                servedAgencies = counties.Select(x => x.Employer).ToList();
            }
            if (servedAgencies.Count > 0)
            {
                selectAgenciesForBidText = "Please proceed to select agencies that will bid for this service request.";
                servedAgenciesText = $"<p>These are the Agencies that service that location:</p>";
                var index = 1;
                foreach (var agency in servedAgencies)
                {
                    servedAgenciesText += $"<p>({index}) {agency.Name}</p>";
                    index++;
                }
            }
            else
            {
                servedAgenciesText = "Currently there are no agencies which are servicing that location.";
            }

            foreach (var admin in admins)
            {
                var adminData = await _adminService.GetAdminById(admin.Id);
                await SendNewServiceRequestEmailToAdmin(adminData, jobData, servedAgenciesText, selectAgenciesForBidText);
            }
            return job;
        }

        public async Task Delete(Job job)
        {
            await _jobRepository.Delete(job);
        }

        public async Task Update(Job job)
        {
            await _jobRepository.Update(job);
        }

        public async Task<Job> ClientRating(ClientRatingViewModel model)
        {
            var job = await _jobRepository.GetById(model.JobId);
            if (job != null)
            {
                job.ClientRatingToAgency = model.ClientRatingToAgency;
                job.ClientRatingToNurse = model.ClientRatingToNurse;
                await _jobRepository.Update(job);
            }
            return job;
        }

        public async Task<Job> CreateServiceRequest(CreateServiceRequestViewModel model)
        {
            CareRecipient careRecipient = _mapper.Map<CareRecipient>(model);
            careRecipient.Status = CareRecipientStatus.Admitted.ToString();
            careRecipient.Visibility = CareRecipientVisibility.Active.ToString();
            var careRecipientId = await _careRecipientService.Add(careRecipient);

            Job job = new Job
            {
                EmployerId = model.EmployerId,
                CareRecipientId = careRecipientId,
                Description = model.MoreInformation,
                PostedTime = DateTime.UtcNow,
                StatusId = 5,
                JobTitleId = model.JobTitleId,
                ClientPaymentMethod = model.ClientPaymentMethod,
                IsDischargeSummaryRequired  = model.IsDischargeSummaryRequired,
                IsInitialAssesstmentRequired = model.IsInitialAssesstmentRequired,
                IsSupervisoryVisitsRequired = model.IsSupervisoryVisitsRequired,
                ReAssessmentFrequency = model.ReAssessmentFrequency,
                ReVisitFrequency = model.ReVisitFrequency,
                CareGiverRate = model.CareGiverRate
            };
            await _jobRepository.Add(job);
            var jobTitle = await _jobTitleService.GetById(model.JobTitleId);
            job.JobTitle = jobTitle;
            return job;
        }

        public async Task<Job> CreateDuplicateServiceRequest(int jobId, string employerId)
        {
            var job = await GetById(jobId);
            if (job == null)
                return null;
            Job duplicateServiceRequest = new Job
            {
                EmployerId = employerId,
                CareRecipientId = job.CareRecipientId,
                Description = job.Description,
                PostedTime = DateTime.UtcNow,
                StatusId = 5,
                JobTitleId = job.JobTitleId,
                ClientId = job.ClientId,
                ClientPaymentMethod = job.ClientPaymentMethod,
                IsDischargeSummaryRequired = job.IsDischargeSummaryRequired
            };
            duplicateServiceRequest.JobApplyForAgencies.Add(new JobApplyForAgency
            {
                EmployerId = employerId,
                StatusId = 13
            });
            
            if(job.TaskList != null && job.TaskList.Count > 0)
            {
                foreach(var task in job.TaskList)
                {
                    duplicateServiceRequest.TaskList.Add(new TaskList
                    {
                        EmployerId =employerId,
                        TaskName =  task.TaskName,
                        TaskDescription = task.TaskDescription,
                        Date = task.Date
                    });
                }
                job.SentToNurse = true;
            }
            await _jobRepository.Add(duplicateServiceRequest);
            return duplicateServiceRequest;
        }

        public async Task<List<GetCareRecipientListResponseModel>> GetCareRecipientsByEmployerId(string employerId)
        {
            var jobs = await _jobRepository.GetJobsByEmployerId(employerId);
            return _mapper.Map<List<GetCareRecipientListResponseModel>>(jobs.DistinctBy(x => x.CareRecipientId));
        }

        public async Task<List<GetServiceRequestListResponseModel>> GetJobsForEmployer(string employerId)
        {
            var jobs = await _jobRepository.GetJobsForEmployer(employerId);
            return _mapper.Map<List<GetServiceRequestListResponseModel>>(jobs);
        }

        public async Task<List<GetServiceRequestListResponseModel>> GetJobsForNurse(string nurseId)
        {
            var jobs = await _jobRepository.GetJobsForNurse(nurseId);
            return _mapper.Map<List<GetServiceRequestListResponseModel>>(jobs);
        }

        public async Task<List<GetServiceRequestListResponseModel>> GetJobsForClient(string clientId)
        {
            var jobs = await _jobRepository.GetJobsForClient(clientId);
            return _mapper.Map<List<GetServiceRequestListResponseModel>>(jobs);
        }

        public async Task<List<GetServiceRequestListResponseModel>> GetJobsForAdmin()
        {
            var jobs = await _jobRepository.GetJobsForAdmin();
            return _mapper.Map<List<GetServiceRequestListResponseModel>>(jobs);
        }

        public async Task<bool> DeleteServiceRequest(int jobId)
        {
            var job = await _jobRepository.GetById(jobId);
            if (job == null)
                return false;
            await _jobRepository.Delete(job);
            return true;
        }

        public async Task<CreateServiceRequestViewModel> GetServiceRequestById(int jobId)
        {
            var job = await _jobRepository.GetServiceRequestById(jobId);
            return _mapper.Map<CreateServiceRequestViewModel>(job);
        }

        public async Task<bool> UpdateServiceRequest(CreateServiceRequestViewModel model)
        {
            var job = await _jobRepository.GetServiceRequestById(model.JobId);
            if (job == null)
                return false;
            if(model.IsDischargeSummaryRequired)
            {
                if(job.DischargeSummaries.Any())
                {
                    job.DischargeSummaries.FirstOrDefault().NurseId = model.DischargeSummaryAssignedNurse;
                }
                else
                {
                    job.DischargeSummaries.Add(new DischargeSummary
                    {
                        JobId = model.JobId,
                        EmployerId = model.EmployerId,
                        DateCreated = DateTime.UtcNow,
                        NurseId = model.DischargeSummaryAssignedNurse
                    });
                }
            }
            if(!model.IsDischargeSummaryRequired && job.DischargeSummaries.Any())
            {
                job.DischargeSummaries.Remove(job.DischargeSummaries.FirstOrDefault());
            }
            if (model.IsSupervisoryVisitsRequired)
            {
                if (job.SupervisoryVisits.Any())
                {
                    job.SupervisoryVisits.FirstOrDefault().NurseId = model.SupervisoryVisitAssignedNurse;
                }
                else
                {
                    job.SupervisoryVisits.Add(new SupervisoryVisit
                    {
                        JobId = model.JobId,
                        EmployerId = model.EmployerId,
                        DateCreated = DateTime.UtcNow,
                        NurseId = model.SupervisoryVisitAssignedNurse
                    });
                }
            }
            if (!model.IsSupervisoryVisitsRequired && job.SupervisoryVisits.Any())
            {
                job.SupervisoryVisits.Remove(job.SupervisoryVisits.FirstOrDefault());
            }
            if (model.IsInitialAssesstmentRequired)
            {
                if (job.AdultAssessments.Any())
                {
                    job.AdultAssessments.FirstOrDefault().NurseId = model.InitialAssessmentAssignedNurse;
                }
                else
                {
                    job.AdultAssessments.Add(new AdultAssessment
                    {
                        JobId = model.JobId,
                        EmployerId = model.EmployerId,
                        NurseId = model.InitialAssessmentAssignedNurse
                    });
                }
            }
            if (!model.IsInitialAssesstmentRequired && job.AdultAssessments.Any())
            {
                job.AdultAssessments.Remove(job.AdultAssessments.FirstOrDefault());
            }

            job.IsDischargeSummaryRequired = model.IsDischargeSummaryRequired;
            job.IsSupervisoryVisitsRequired = model.IsSupervisoryVisitsRequired;
            job.ReVisitFrequency = model.ReVisitFrequency;
            job.IsInitialAssesstmentRequired = model.IsInitialAssesstmentRequired;
            job.ReAssessmentFrequency = model.ReAssessmentFrequency;
            job.Description = model.MoreInformation;
            job.JobTitleId = model.JobTitleId;
            job.ClientPaymentMethod = model.ClientPaymentMethod;
            job.CareGiverRate = model.CareGiverRate;

            job.CareRecipient.StateId = model.StateId;
            job.CareRecipient.CityId = model.CityId;
            job.CareRecipient.ServiceListId = model.ServiceListId;
            job.CareRecipient.FirstName = model.FirstName;
            job.CareRecipient.MiddleInitial = model.MiddleInitial;
            job.CareRecipient.LastName = model.LastName;
            job.CareRecipient.Age = Convert.ToInt32(model.Age);
            job.CareRecipient.AddressLine1 = model.AddressLine1;
            var city = await _cityService.GetById((int)model.CityId);
            var state = await _stateService.GetById((int)model.StateId);
            var latitudeAndLongitude = GetLatitudeAndLongitude.GetLatLogFromAddress(model.AddressLine1, city.Name, state.Name, city.ZipCode);
            if (latitudeAndLongitude != null)
            {
                job.CareRecipient.Latitude = latitudeAndLongitude.Latitude;
                job.CareRecipient.Longitude = latitudeAndLongitude.Longitude;
            }
            job.CareRecipient.PostalCode = model.PostalCode;
            job.CareRecipient.Frequency = model.Frequency;
            job.CareRecipient.GenderPreference = model.GenderPreference;
            job.CareRecipient.WhenToStart = model.Date.ToString();
            job.CareRecipient.EndDate = model.EndDate.ToString();
            job.CareRecipient.MoreInformation = model.MoreInformation;
            job.CareRecipient.TotalHours = model.TotalHours.ToString();
            job.CareRecipient.ResponsiblePartyName = model.ResponsiblePartyName;
            job.CareRecipient.ResponsiblePartyEmail = model.ResponsiblePartyEmail;
            job.CareRecipient.ResponsiblePartyRelation = model.ResponsiblePartyRelation;
            job.CareRecipient.ResponsiblePartyTelephoneNumber = model.ResponsiblePartyTelephoneNumber;
            job.CareRecipient.IsResponsiblePartySameAsCareRecipient = model.IsResponsiblePartySameAsCareRecipient;
            job.CareRecipient.EmailAddress = model.EmailAddress;

            if (job.JobApplies.Any() && !string.IsNullOrEmpty(model.CareGiver))
            {
                job.JobApplies.FirstOrDefault().NurseId = model.CareGiver;
            }
            if (job.JobApplyForAgencies.Any())
            {
                job.JobApplyForAgencies.FirstOrDefault().PrefferedRate = model.AgencyRate.ToString();
            }
            await _jobRepository.Update(job);
            return true;
        }

        private async Task<bool> SendNewServiceRequestEmailToAdmin(Admin admin, Job job, string servedAgenciesText, string selectAgenciesForBidText)
        {
            string emailBody = string.Empty;
            emailBody = Utility.GetEmailTemplateValue(Environment.CurrentDirectory + @"\\EmailTemplates\NewServiceRequestAdminTemplate.xml", "NewServiceRequestEmail/Body");
            emailBody = emailBody.Replace("@@clientName", $"{job.CareRecipient.FirstName} {job.CareRecipient.LastName}");
            emailBody = emailBody.Replace("@@service", job.CareRecipient.ServiceList.Name);
            emailBody = emailBody.Replace("@@location", job.CareRecipient.AddressLine1);
            emailBody = emailBody.Replace("@@startDate", Convert.ToDateTime(job.CareRecipient.WhenToStart).Date.ToShortDateString());
            emailBody = emailBody.Replace("@@endDate", Convert.ToDateTime(job.CareRecipient.EndDate).Date.ToShortDateString());
            emailBody = emailBody.Replace("@@totalHours", job.CareRecipient.TotalHours);
            emailBody = emailBody.Replace("@@shiftTimings", $"{Convert.ToDateTime(job.CareRecipient.WhenToStart).ToShortTimeString()}-{Convert.ToDateTime(job.CareRecipient.EndDate).ToShortTimeString()}");
            emailBody = emailBody.Replace("@@request", job.JobTitle.Title);
            emailBody = emailBody.Replace("@@frequency", job.CareRecipient.Frequency);
            emailBody = emailBody.Replace("@@servedAgencies", servedAgenciesText);
            emailBody = emailBody.Replace("@@selectAgenciesBid", selectAgenciesForBidText);
            emailBody = emailBody.Replace("@@adminName", admin.Name);
            return Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.NewServiceRequest), admin.Email, emailBody);
        }
        #endregion
    }
}
