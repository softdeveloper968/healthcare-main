using Contracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Common;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using GaHealthcareNurses.Entity;
using AutoMapper;

namespace Services
{
    public class JobApplyService : IJobApplyService
    {
        private IJobApplyRepository _jobApplyRepository;
        private IStatusService _statusService;
        private IJobService _jobService;
        private INurseInboxService _nurseInboxService;
        private IJobApplyForAgencyService _jobApplyForAgencyService;
        private IMapper _mapper;

        #region Constructor for JobApplyService
        public JobApplyService(IJobApplyRepository jobApplyRepository, IStatusService statusService, IJobService jobService, INurseInboxService nurseInboxService, IJobApplyForAgencyService jobApplyForAgencyService, IMapper mapper)
        {
            _jobApplyRepository = jobApplyRepository;
            _statusService = statusService;
            _jobService = jobService;
            _nurseInboxService = nurseInboxService;
            _jobApplyForAgencyService = jobApplyForAgencyService;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<IEnumerable<JobApply>> Get()
        {
            return await _jobApplyRepository.Get();
        }

        public async Task<JobApply> GetById(int id)
        {
            return await _jobApplyRepository.GetById(id);
        }

        public async Task<IEnumerable<JobApply>> GetByJobId(int id)
        {
            return await _jobApplyRepository.GetByJobId(id);
        }

        public async Task<IEnumerable<JobApply>> GetByNurseId(string id)
        {
            return await _jobApplyRepository.GetByNurseId(id);
        }

        public async Task<IEnumerable<JobApply>> GetByStatusId(string nurseId,int statusId)
        {
            return await _jobApplyRepository.GetByStatusId(nurseId,statusId);
        }

        public async Task<IEnumerable<JobApply>> GetByJobIdAndStatusId(int jobId, int statusId)
        {
            return await _jobApplyRepository.GetByJobIdAndStatusId(jobId, statusId);
        }

        public async Task<JobApply> GetByJobIdAndNurseId(int jobId, string nurseId)
        {
            return await _jobApplyRepository.GetByJobIdAndNurseId(jobId, nurseId);
        }

        public async Task<JobApply> HireNurse(SendJobOfferToNurseViewModel model)
        {
            var offerAlreadySent = await GetByJobIdAndStatusId(model.JobId, 2);
            if(offerAlreadySent != null && offerAlreadySent.ToList().Count > 0)
            {
                return null;
            }

            var job = await GetByJobIdAndNurseId(model.JobId, model.NurseId);
            if (job != null)
            {
                job.StatusId = 2;
                job.OfferedRate = model.OfferedRate.ToString();
                await _jobApplyRepository.Update(job);
            }

            var jobData = await _jobService.GetById(model.JobId);

            await SendJobOfferEmailToNurse(jobData, job.Nurse, $"${job.OfferedRate}/Hour");

            NurseInbox message = new NurseInbox
            {
                NurseId = model.NurseId,
                EmployerId = jobData.EmployerId,
                JobId = jobData.JobId,
                Subject = EnumHelper.GetDescription(NurseInboxSubject.JobApplicationSuccessful),
                SentDate = DateTime.UtcNow,
                Message = $"Congratulations! Your application for the service request ({jobData.JobTitle.Title}) for Care Recipient - {jobData.CareRecipient.FirstName} {jobData.CareRecipient.LastName} has been accepted."
            };
            await _nurseInboxService.AddMessage(message);
            Utility.SendMailToUser(message.Subject, job.Nurse.EmailAddress, message.Message);

            return job;
        }

        public async Task<JobApply> Add(JobApply job)
        {
            job.StatusId = 4;
            var status = await _statusService.GetById((int)job.StatusId);
            job.Status = status;
            job.AcceptJobDescriptionAndPolicies = true;
            return await _jobApplyRepository.Add(job);
        }

        public async Task Delete(JobApply job)
        {
            await _jobApplyRepository.Delete(job);
        }

        public async Task<JobApply> Update(JobApplyUpdateViewModel job)
        {
            var jobApplied = await _jobApplyRepository.GetById(job.Id);
            if (jobApplied != null)
            {
                jobApplied.JobId = job.JobId;
                jobApplied.NurseId = job.NurseId;
                jobApplied.OfferedRate = job.OfferedRate;
                jobApplied.PrefferedRate = job.PrefferedRate;
                jobApplied.RequiredHours = job.RequiredHours;
                jobApplied.StatusId = job.StatusId;
                jobApplied.Status = await _statusService.GetById((int)jobApplied.StatusId);
                var jobAppliedData= await _jobApplyRepository.Update(jobApplied);

                string templatePath = Environment.CurrentDirectory + @"\\EmailTemplates\JobOfferReplyTemplate.xml";
                var message = await this.SendEmailToUser(jobAppliedData, templatePath, EmailTemplateType.JobOffer.ToString());
                if (message == true)
                {
                    return jobAppliedData;
                }
            }
            return jobApplied;

        }

        public async Task UpdateJobApply(JobApply job)
        {
            await _jobApplyRepository.Update(job);
        }

        public async Task<JobApply> NurseFeedback(FeedbackViewModel nurseFeedback)
        {
            var jobApply = await _jobApplyRepository.GetById(nurseFeedback.JobApplyId);
            if (jobApply != null)
            {
                jobApply.NurseFeedback = nurseFeedback.Feedback;
                jobApply.NurseRating = nurseFeedback.Rating;
                return await _jobApplyRepository.Update(jobApply);
            }
            return jobApply;
        }

        public async Task<JobApply> ClientFeedback(FeedbackViewModel clientFeedback)
        {
            var jobApply = await _jobApplyRepository.GetById(clientFeedback.JobApplyId);
            if (jobApply != null)
            {
                jobApply.ClientFeedback = clientFeedback.Feedback;
                jobApply.ClientRating = clientFeedback.Rating;
                return await _jobApplyRepository.Update(jobApply);
            }
            return jobApply;
        }

        public async Task<JobApply> PermissionForShareDocuments(PermissionToShareDocumentsViewModel permissionToShareDocuments)
        {
            var jobApply = await _jobApplyRepository.GetById(permissionToShareDocuments.JobApplyId);
            if (jobApply != null)
            {
                jobApply.AcceptJobDescriptionAndPolicies = permissionToShareDocuments.AcceptJobDescriptionAndPolicies;
                jobApply.DocumentsCanBeShared = permissionToShareDocuments.DocumentsCanBeShared;
                jobApply.SSNCanBeShared = permissionToShareDocuments.SSNCanBeShared;
                jobApply.CNACanBeShared = permissionToShareDocuments.CNACanBeShared;
                jobApply.CPRCanBeShared = permissionToShareDocuments.CPRCanBeShared;
                jobApply.DrivingLicenseCanBeShare = permissionToShareDocuments.DrivingLicenseCanBeShare;
                jobApply.TBResultsCanBeShared = permissionToShareDocuments.TBResultsCanBeShared;
                jobApply.W4CanBeShared = permissionToShareDocuments.W4CanBeShared;
                jobApply.HiringDisclosuresCanBeShared = permissionToShareDocuments.HiringDisclosuresCanBeShared;
                jobApply.HiringPreScreeningCanBeShared = permissionToShareDocuments.HiringPreScreeningCanBeShared;
                jobApply.G4CanBeShared = permissionToShareDocuments.G4CanBeShared;
                return await _jobApplyRepository.Update(jobApply);
            }
            return jobApply;
        }

        public async Task<string> CompleteJob(CompleteServiceRequestFromMobileRequestModel model)
        {
            var nurseJobRelation = await GetByJobIdAndNurseId(model.JobId, model.NurseId);
            if (nurseJobRelation == null)
                return "Nurse is not hired for this job";
            nurseJobRelation.StatusId = 7;
            await _jobApplyRepository.Update(nurseJobRelation);
            var hiredAgencyData = await _jobApplyForAgencyService.GetByJobIdAndStatusId(model.JobId, 13);
            if (hiredAgencyData != null && hiredAgencyData.ToList().Count > 0)
            {
                hiredAgencyData.FirstOrDefault().StatusId = 14;
                await _jobApplyForAgencyService.Update(hiredAgencyData.FirstOrDefault());
            }
            return "Job is completed";
        }

        public async Task<List<ActiveServiceRequestsResponseModel>> GetActiveServiceRequestsOfNurse(string nurseId)
        {
           var jobs =  await GetByStatusId(nurseId, 1);
            return _mapper.Map<List<ActiveServiceRequestsResponseModel>>(jobs);
        }

        public async Task<bool> SendEmailToUser(JobApply job, string templatePath, string type)
        {
            try
            {
                string emailBody = string.Empty;
                emailBody = Utility.GetEmailTemplateValue(templatePath, "JobOfferReplyEmail/Body");
                emailBody = emailBody.Replace("@@@agencyName", job.Job.Employer.Name);
                emailBody = emailBody.Replace("@@@nurse", job.Nurse.FirstName + " " + job.Nurse.LastName);
                emailBody = emailBody.Replace("@@@reply", job.Status.Name);
                Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.JobOffer), job.Job.Employer.EmailAddress, emailBody);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<bool> SendJobOfferEmailToNurse(Job job, Nurse nurse, string offeredRate)
        {
            string emailBody = string.Empty;
            emailBody = Utility.GetEmailTemplateValue(Environment.CurrentDirectory + @"\\EmailTemplates\JobOfferNurseTemplate.xml", "JobOfferEmail/Body");
            emailBody = emailBody.Replace("@@clientName", $"{job.CareRecipient.FirstName} {job.CareRecipient.LastName}");
            emailBody = emailBody.Replace("@@service", job.CareRecipient.ServiceList.Name);
            emailBody = emailBody.Replace("@@location", job.CareRecipient.AddressLine1);
            emailBody = emailBody.Replace("@@startDate", Convert.ToDateTime(job.CareRecipient.WhenToStart).Date.ToShortDateString());
            emailBody = emailBody.Replace("@@endDate", Convert.ToDateTime(job.CareRecipient.EndDate).Date.ToShortDateString());
            emailBody = emailBody.Replace("@@totalHours", job.CareRecipient.TotalHours);
            emailBody = emailBody.Replace("@@shiftTimings", $"{Convert.ToDateTime(job.CareRecipient.WhenToStart).ToShortTimeString()}-{Convert.ToDateTime(job.CareRecipient.EndDate).ToShortTimeString()}");
            emailBody = emailBody.Replace("@@request", job.JobTitle.Title);
            emailBody = emailBody.Replace("@@frequency", job.CareRecipient.Frequency);
            emailBody = emailBody.Replace("@@nurseName", $"{nurse.FirstName} {nurse.LastName}");
            emailBody = emailBody.Replace("@@agencyName", job.Employer.Name);
            emailBody = emailBody.Replace("@@offeredRate", offeredRate);
            emailBody = emailBody.Replace("@@agencyPhone", $"{job.Employer.TelephoneNo.Substring(0,3)}-{job.Employer.TelephoneNo.Substring(3, 3)}-{job.Employer.TelephoneNo.Substring(6, 4)}");
            return Utility.SendMailToUser($"{EnumHelper.GetDescription(EmailSubjectRole.NurseJobOffer)}{nurse.FirstName} {nurse.LastName}", nurse.EmailAddress, emailBody);
        }
        #endregion
    }
}
