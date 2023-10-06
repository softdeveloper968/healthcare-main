using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Common;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Services
{
    public class JobInvitationService : IJobInvitationService
    {
        private readonly IJobApplyForAgencyService _jobApplyForAgencyService;
        private readonly IJobService _jobService;
        private readonly IEmployerService _employerService;

        #region Constructor for JobInvitationService
        public JobInvitationService(IJobApplyForAgencyService jobApplyForAgencyService, IEmployerService employerService, IJobService jobService)
        {
            _jobApplyForAgencyService = jobApplyForAgencyService;
            _employerService = employerService;
            _jobService = jobService;
        }
        #endregion
        public async Task<int> SendJobInvitation(JobInvitationViewModel model)
        {
            int emailCount = 0;
            var job = await _jobService.GetById(model.JobId);
            foreach (var agency in model.Agencies)
            {
                var agencyData = await _employerService.GetById(agency);
                string templatePath = Environment.CurrentDirectory + @"\\EmailTemplates\JobInvitationTemplate.xml";
                var message = await SendEmailToUser(agencyData, job.JobTitle.Title, templatePath, EmailTemplateType.JobInvitation.ToString());
                if (message == true)
                {
                    emailCount += 1;

                    JobApplyForAgency jobApply = new JobApplyForAgency
                    {
                        JobId = job.JobId,
                        EmployerId = agency,
                        StatusId = 11
                    };
                    await _jobApplyForAgencyService.Add(jobApply);
                }
            }

            return emailCount;
        }

        private async Task<bool> SendEmailToUser(Employer employer, string jobTitle, string templatePath, string type)
        {
            try
            {
                string emailBody = string.Empty;
                emailBody = Utility.GetEmailTemplateValue(templatePath, "JobInvitationEmail/Body");
                emailBody = emailBody.Replace("@@@username", employer.Name);
                emailBody = emailBody.Replace("@@@job", jobTitle);
                emailBody = emailBody.Replace("@@@id", employer.Id);

                Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.Invitation), employer.EmailAddress, emailBody);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
