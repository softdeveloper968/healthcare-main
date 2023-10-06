using AutoMapper;
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

namespace Services
{
    public class JobApplyForAgencyService : IJobApplyForAgencyService
    {
        private IJobApplyForAgencyRepository _jobApplyForAgencyRepository;
        private IStatusService _statusService;
        private IJobService _jobService;
        private IMapper _mapper;
        private IAdminService _adminService;

        #region Constructor for JobApplyForAgencyService
        public JobApplyForAgencyService(IJobApplyForAgencyRepository jobApplyForAgencyRepository, IStatusService statusService, IJobService jobService, IMapper mapper, IAdminService adminService)
        {
            _jobApplyForAgencyRepository = jobApplyForAgencyRepository;
            _statusService = statusService;
            _jobService = jobService;
            _mapper = mapper;
            _adminService = adminService;
        }
        #endregion

        #region Implementing Interface
        public async Task<IEnumerable<JobApplyForAgency>> Get()
        {
            return await _jobApplyForAgencyRepository.Get();
        }

        public async Task<JobApplyForAgency> GetById(int id)
        {
            return await _jobApplyForAgencyRepository.GetById(id);
        }

        public async Task<IEnumerable<JobApplyForAgency>> GetByJobId(int id)
        {
            return await _jobApplyForAgencyRepository.GetByJobId(id);
        }

        public async Task<IEnumerable<JobApplyForAgency>> GetByJobIdAndStatusId(int jobId, int statusId)
        {
            return await _jobApplyForAgencyRepository.GetByJobIdAndStatusId(jobId, statusId);
        }

        public async Task<IEnumerable<JobApplyForAgency>> GetByEmployerId(string id)
        {
            return await _jobApplyForAgencyRepository.GetByEmployerId(id);
        }

        public async Task<IEnumerable<JobApplyForAgency>> GetByStatusId(string employerId, int statusId)
        {
            return await _jobApplyForAgencyRepository.GetByStatusId(employerId, statusId);
        }

        public async Task<List<GetActiveServiceRequestsResponseModel>> GetActiveServiceRequests(string employerId)
        {
            var serviceRequests = await GetByStatusId(employerId, 13);
            List<GetActiveServiceRequestsResponseModel> activeServiceRequests = _mapper.Map<List<GetActiveServiceRequestsResponseModel>>(serviceRequests);
            return activeServiceRequests;
        }

        public async Task<JobApplyForAgency> Add(JobApplyForAgency job)
        {
            return await _jobApplyForAgencyRepository.Add(job);
        }

        public async Task Delete(JobApplyForAgency job)
        {
            await _jobApplyForAgencyRepository.Delete(job);
        }

        public async Task<JobApplyForAgency> Update(JobApplyForAgency job)
        {
            return await _jobApplyForAgencyRepository.Update(job);
        }

        public async Task<JobApplyForAgency> ApplyJob(int jobApplyId, string prefferedRate)
        {
            var jobApplied = await _jobApplyForAgencyRepository.GetById(jobApplyId);
            if (jobApplied != null)
            {
                string templatePath = Environment.CurrentDirectory + @"\\EmailTemplates\JobAppliedTemplate.xml";
                var message = await this.SendEmailToUser(jobApplied, prefferedRate, templatePath, EmailTemplateType.JobApplied.ToString());
                if (message == true)
                {
                    jobApplied.PrefferedRate = prefferedRate;
                    jobApplied.StatusId = 12;
                    return await _jobApplyForAgencyRepository.Update(jobApplied);
                }
                return null;
            }
            return jobApplied;
        }

        public async Task<bool> SendEmailToUser(JobApplyForAgency job, string prefferedRate, string templatePath, string type)
        {
            try
            {
                string emailBody = string.Empty;
                emailBody = Utility.GetEmailTemplateValue(templatePath, "JobInvitationEmail/Body");
                emailBody = emailBody.Replace("@@@client", job.Job.Client.FirstName);
                emailBody = emailBody.Replace("@@@careRecipient", $"{job.Job.CareRecipient.FirstName} {job.Job.CareRecipient.LastName}");
                emailBody = emailBody.Replace("@@@prefferedRate", $"${prefferedRate}/hour");
                emailBody = emailBody.Replace("@@@agency", job.Employer.Name);
                emailBody = emailBody.Replace("@@@serviceRequest", job.Job.JobTitle.Title);
                emailBody = emailBody.Replace("@@@websiteLink", !string.IsNullOrEmpty(job.Employer.AgencyWebsite) ? $"Please click on our website to check us out! <a href='{job.Employer.AgencyWebsite}'><b>WEBSITE LINK</b></a>" : string.Empty);
                emailBody = emailBody.Replace("@@@logo", !string.IsNullOrEmpty(job.Employer.Logo) ? $"<img style='max-height: 100px; max-width: 100px;' src='{job.Employer.Logo}' alt='{job.Employer.Name}' />" : string.Empty);
                emailBody = emailBody.Replace("@@@applyId", job.Id.ToString());
                Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.InvitationAccept), job.Job.Client.EmailAddress, emailBody);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JobApplyForAgency> HireAgency(int jobApplyId)
        {
            var jobApply = await _jobApplyForAgencyRepository.GetById(jobApplyId);
            if (jobApply != null)
            {
                string templatePath = Environment.CurrentDirectory + @"\\EmailTemplates\JobOfferTemplate.xml";
                var message = await SendEmailToAgency(jobApply, templatePath, EmailTemplateType.JobOffer.ToString());
                if (message == true)
                {
                    jobApply.StatusId = 13;
                    //jobApply.Job.SentToNurse = true;
                    jobApply.Job.EmployerId = jobApply.EmployerId;
                    await _jobApplyForAgencyRepository.Update(jobApply);
                }

                //For sending email to admin.
                var admins = await _adminService.GetAllAdmins();
                var getAgenciesBids = await GetByJobIdAndStatusId(jobApply.JobId, 12);
                var agenciesBidsText = string.Empty;
                var index = 1;
                agenciesBidsText = $"<p>{index}) {jobApply.Employer.Name}  ${jobApply.PrefferedRate}/hr <b>(Selected)</b></p>";
                if (getAgenciesBids.ToList().Count > 0)
                {
                    foreach (var agencyBid in getAgenciesBids)
                    {
                        index++;
                        agenciesBidsText += $"<p>{index}) {agencyBid.Employer.Name}  ${agencyBid.PrefferedRate}/hr</p>";
                    }
                }
                foreach (var admin in admins)
                {
                    var adminData = await _adminService.GetAdminById(admin.Id);
                    await CareRecipientHireAgencyEmailToAdmin(adminData, jobApply.Job, agenciesBidsText);
                }
            }
            return jobApply;
        }

        public async  Task<List<GetHiredNursesResponseModel>> GetHiredNurses(string employerId)
        {
            return await _jobApplyForAgencyRepository.GetHiredNurses(employerId);
        }

        public async Task<bool> SendEmailToAgency(JobApplyForAgency job,string templatePath,string type)
        {
            try
            {
                string emailBody = string.Empty;
                emailBody = Utility.GetEmailTemplateValue(templatePath, "JobOfferEmail/Body");
                emailBody = emailBody.Replace("@@@name", job.Employer.Name);
                emailBody = emailBody.Replace("@@@client", job.Job.Client.FirstName + " " + job.Job.Client.LastName);

                Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.JobOffer), job.Employer.EmailAddress, emailBody);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<bool> CareRecipientHireAgencyEmailToAdmin(Admin admin, Job job, string agencyBidsText)
        {
            string emailBody = string.Empty;
            emailBody = Utility.GetEmailTemplateValue(Environment.CurrentDirectory + @"\\EmailTemplates\ServiceRequestAwardedAdminTemplate.xml", "ServiceRequestAwardedEmail/Body");
            emailBody = emailBody.Replace("@@serviceRequest", job.JobTitle.Title);
            emailBody = emailBody.Replace("@@client", $"{job.Client.FirstName} {job.Client.LastName}");
            emailBody = emailBody.Replace("@@city", job.CareRecipient.City.Name);
            emailBody = emailBody.Replace("@@hiredAgency", job.Employer.Name);
            emailBody = emailBody.Replace("@@agencyBids", agencyBidsText);
            emailBody = emailBody.Replace("@@adminName", admin.Name);
            return Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.ServiceRequestAwarded), admin.Email, emailBody);
        }
        #endregion
    }
}
