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

namespace Services
{
    public class NurseCommentService : INurseCommentService
    {
        private INurseService _nurseService;
        private IMapper _mapper;
        private IJobService _jobService;
        private IJobApplyService _jobApplyService;
        private IJobApplyForAgencyService _jobApplyForAgencyService;
        private INurseCommentRepository _nurseCommentRepository;

        #region Constructor for NurseCommentService
        public NurseCommentService(INurseService nurseService, IJobService jobService, INurseCommentRepository nurseCommentRepository, IJobApplyService jobApplyService, IJobApplyForAgencyService jobApplyForAgencyService, IMapper mapper)
        {
            _nurseService = nurseService;
            _jobService = jobService;
            _jobApplyService = jobApplyService;
            _jobApplyForAgencyService = jobApplyForAgencyService;
            _nurseCommentRepository = nurseCommentRepository;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddComment(AddCommentViewModel model)
        {
            if (string.IsNullOrEmpty(model.NurseId) || model.JobId == 0)
                return false;
            var nurseData = await _nurseService.GetById(model.NurseId);
            if (nurseData == null)
                return false;
            var jobData = await _jobService.GetById(model.JobId);
            if (jobData == null)
                return false;
            var nurseJobRelation = await _jobApplyService.GetByJobIdAndNurseId(model.JobId, model.NurseId);
            if (nurseJobRelation == null)
                return false;
            var hiredEmployerData = await _jobApplyForAgencyService.GetByJobIdAndStatusId(model.JobId, 13);
            if (hiredEmployerData == null || hiredEmployerData.ToList().Count == 0)
                return false;

            var result = await _nurseCommentRepository.AddComment(new NurseComment
            {
                NurseId = model.NurseId,
                JobId = model.JobId,
                Comment = model.Comment,
                IsUrgent = model.IsUrgent,
                EmployerId = hiredEmployerData.FirstOrDefault().EmployerId,
                CommentDate = DateTime.UtcNow
            });
            if (model.IsUrgent)
            {
                await SendEmailToAgency(hiredEmployerData.FirstOrDefault().Employer, $"{nurseData.FirstName} {nurseData.LastName}", $"{jobData.CareRecipient.FirstName} {jobData.CareRecipient.LastName}", model.Comment);
            }
            return result;
        }

        public async Task<List<GetNurseCommentResponseModel>> GetCommentsForNurse(string nurseId, int jobId)
        {
            var nurseComments = await _nurseCommentRepository.GetCommentsForNurse(nurseId, jobId);
            return _mapper.Map<List<GetNurseCommentResponseModel>>(nurseComments);
        }

        public async Task<List<GetAgenyCommentResponseModel>> GetCommentsForAgency(string employerId)
        {
            var comments = await _nurseCommentRepository.GetCommentsForAgency(employerId);
            return _mapper.Map<List<GetAgenyCommentResponseModel>>(comments);
        }

        public async Task<bool> UpdateAgencyResponse(AgencyResponseRequestModel model)
        {
            var nurseComment = await _nurseCommentRepository.GetNurseCommentById(model.NurseCommentId);
            if (nurseComment != null)
            {
                nurseComment.AgencyResponse = model.AgencyResponse;
                nurseComment.ResponseDate = DateTime.UtcNow;
                return await _nurseCommentRepository.UpdateComment(nurseComment);
            }
            return false;
        }

        public async Task<bool> ReadNurseComment(int nurseCommentId)
        {
            var nurseComment = await _nurseCommentRepository.GetNurseCommentById(nurseCommentId);
            if(nurseComment != null)
            {
                nurseComment.IsRead = true;
                return await _nurseCommentRepository.UpdateComment(nurseComment); 
            }
            return false;
        }

        private async Task<bool> SendEmailToAgency(Employer employer, string nurseName, string clientName, string comment)
        {
            string templatePath = Environment.CurrentDirectory + @"\\EmailTemplates\CommentNotificationTemplate.xml";
            string emailBody = Utility.GetEmailTemplateValue(templatePath, "CommentAddedEmail/Body");
            emailBody = emailBody.Replace("@@agencyName", employer.Name);
            emailBody = emailBody.Replace("@@nurseName", nurseName);
            emailBody = emailBody.Replace("@@clientName", clientName);
            emailBody = emailBody.Replace("@@comment", comment);

            return Utility.SendMailToUser($"Message from Nurse: {nurseName}", employer.EmailAddress, emailBody);
        }
        #endregion
    }
}
