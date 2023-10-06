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
    public class AdultAssessmentService : IAdultAssessmentService
    {
        private IAdultAssessmentRepository _adultAssessmentRepository;
        private INurseService _nurseService;
        private IMapper _mapper;
        private IJobService _jobService;
        private INurseInboxService _nurseInboxService;
        private IDiagnosisService _diagnosisService;
        private IWoundService _woundService;

        #region Constructor for AdultAssessmentService
        public AdultAssessmentService(IAdultAssessmentRepository adultAssessmentRepository, INurseService nurseService, IJobService jobService, INurseInboxService nurseInboxService, IDiagnosisService diagnosisService, IWoundService woundService, IMapper mapper)
        {
            _adultAssessmentRepository = adultAssessmentRepository;
            _nurseService = nurseService;
            _jobService = jobService;
            _nurseInboxService = nurseInboxService;
            _diagnosisService = diagnosisService;
            _woundService = woundService;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> CreateAssessment(AdultAssessment model)
        {
            return await _adultAssessmentRepository.AddAssessment(model);
        }

        public async Task<List<GetAdultAssessmentListResponseModel>> GetAssessmentByNurseId(string nurseId)
        {
            var assessments = await _adultAssessmentRepository.GetAssessmentByNurseId(nurseId);
            return _mapper.Map<List<GetAdultAssessmentListResponseModel>>(assessments);
        }

        public async Task<List<GetAdultAssessmentListResponseModel>> GetAssessmentByEmployerId(string employerId)
        {
            var assessments = await _adultAssessmentRepository.GetAssessmentByEmployerId(employerId);
            return _mapper.Map<List<GetAdultAssessmentListResponseModel>>(assessments);
        }

        public async Task<AdultAssessmentRequestModel> GetAssessmentById(int id)
        {
            var assessment = await _adultAssessmentRepository.GetAssessmentById(id);
            return _mapper.Map<AdultAssessmentRequestModel>(assessment);
        }

        public async Task<AdultAssessmentRequestModel> GetAssessmentByJobId(int jobId)
        {
            var assessment = await _adultAssessmentRepository.GetAssessmentByJobId(jobId);
            return _mapper.Map<AdultAssessmentRequestModel>(assessment);
        }

        public async Task<bool> UpdateAssessment(AdultAssessmentRequestModel model)
        {
            //var assessment = await _adultAssessmentRepository.GetAssessmentById(model.AdultAssessmentId);
            //if (assessment == null)
            //    return false;

            var wounds = await _woundService.GetByAdultAssessmentId(model.AdultAssessmentId);
            if (wounds != null && wounds.Count > 0)
            {
                await _woundService.DeleteWounds(wounds);
            }
            await _woundService.AddWounds(_mapper.Map<List<Wound>>(model.Wound));

            var diagnoses = await _diagnosisService.GetByAdultAssessmentId(model.AdultAssessmentId);
            if (diagnoses != null && diagnoses.Count > 0)
            {
                await _diagnosisService.DeleteDiagnosis(diagnoses);
            }
            await _diagnosisService.AddDiagnosis(_mapper.Map<List<Diagnosis>>(model.Diagnosis));

            return await _adultAssessmentRepository.UpdateAssessment(_mapper.Map<AdultAssessment>(model));
        }

        public async Task<bool> DeleteAssessment(int id)
        {
            var assessment = await _adultAssessmentRepository.GetAssessmentById(id);
            if (assessment == null)
                return false;
            return await _adultAssessmentRepository.DeleteAssessment(assessment);
        }

        public async Task<bool> AssignAssessment(AssignAdultAssessmentViewModel model)
        {
            var assessment = await _adultAssessmentRepository.GetAssessmentById(model.Id);
            if (assessment == null)
                return false;
            assessment.NurseId = model.NurseId;
            await _adultAssessmentRepository.UpdateAssessment(assessment);
            NurseInbox message = new NurseInbox
            {
                NurseId = model.NurseId,
                EmployerId = assessment.EmployerId,
                JobId = assessment.JobId,
                Subject = EnumHelper.GetDescription(NurseInboxSubject.AssessmentCreated),
                SentDate = DateTime.UtcNow,
                Message = $"The Adult Assessment for the service request ({assessment.Job.JobTitle.Title}) for Care Recipient - {assessment.Job.CareRecipient.FirstName} {assessment.Job.CareRecipient.LastName} has been generated."
            };
            await _nurseInboxService.AddMessage(message);
            var nurseData = await _nurseService.GetById(model.NurseId);
            Utility.SendMailToUser(message.Subject, nurseData.EmailAddress, message.Message);
            return true;
        }
        #endregion
    }
}
