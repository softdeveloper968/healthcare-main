using Contracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GaHealthcareNurses.Entity.Common;
using AutoMapper;
using Services.Helper;

namespace Services
{
    public class SupervisoryVisitService : ISupervisoryVisitService
    {
        private ISupervisoryVisitRepository _supervisoryVisitRepository;
        private INurseService _nurseService;
        private IMapper _mapper;
        private IJobService _jobService;
        private IJobApplyService _jobApplyService;
        private INurseInboxService _nurseInboxService;

        #region Constructor for SupervisoryVisitService
        public SupervisoryVisitService(ISupervisoryVisitRepository supervisoryVisitRepository, INurseService nurseService, IJobService jobService, IJobApplyService jobApplyService, INurseInboxService nurseInboxService, IMapper mapper)
        {
            _supervisoryVisitRepository = supervisoryVisitRepository;
            _nurseService = nurseService;
            _jobService = jobService;
            _jobApplyService = jobApplyService;
            _nurseInboxService = nurseInboxService;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> CreateSupervisoryVisit(SupervisoryVisit model)
        {
            return await _supervisoryVisitRepository.AddSupervisoryVisit(model);
        }

        public async Task<List<GetSupervisoryVisitListResponseModel>> GetSupervisoryVisitByNurseId(string nurseId)
        {
            var supervisoryVisits = await _supervisoryVisitRepository.GetSupervisoryVisitByNurseId(nurseId);
            return _mapper.Map<List<GetSupervisoryVisitListResponseModel>>(supervisoryVisits);
        }

        public async Task<List<GetSupervisoryVisitListResponseModel>> GetSupervisoryVisitByEmployerId(string employerId)
        {
            var supervisoryVisits = await _supervisoryVisitRepository.GetSupervisoryVisitByEmployerId(employerId);
            return _mapper.Map<List<GetSupervisoryVisitListResponseModel>>(supervisoryVisits);
        }

        public async Task<SupervisoryVisitRequestModel> GetSupervisoryVisitById(int id)
        {
            var supervisoryVisit = await _supervisoryVisitRepository.GetSupervisoryVisitById(id);
            var mappedSupervisoryVisitData = _mapper.Map<SupervisoryVisitRequestModel>(supervisoryVisit);
            var hiredNurse = await _jobApplyService.GetByJobIdAndStatusId(supervisoryVisit.JobId, 1);
            if (hiredNurse != null && hiredNurse.ToList().Count > 0)
            {
                mappedSupervisoryVisitData.StaffPersonName = $"{hiredNurse.FirstOrDefault().Nurse.FirstName} {hiredNurse.FirstOrDefault().Nurse.LastName}"; 
            }
            return mappedSupervisoryVisitData;
        }

        public async Task<bool> UpdateSupervisoryVisit(SupervisoryVisitRequestModel model)
        {
            var supervisoryVisit = await _supervisoryVisitRepository.GetSupervisoryVisitById(model.Id);
            if (supervisoryVisit == null)
                return false;
            supervisoryVisit.StaffPersonInHome = model.StaffPersonInHome;
            supervisoryVisit.SupervisoryVisitInfo = model.SupervisoryVisitInfo;
            supervisoryVisit.FollowingPoc = model.FollowingPoc;
            supervisoryVisit.PocChanged = model.PocChanged;
            supervisoryVisit.PatientCompatible = model.PatientCompatible;
            supervisoryVisit.PatientNotifiedOfChange = model.PatientNotifiedOfChange;
            supervisoryVisit.CommunicatesWithPt = model.CommunicatesWithPt;
            supervisoryVisit.CompilesWithInfection = model.CompilesWithInfection;
            supervisoryVisit.HonorsPatientRights = model.HonorsPatientRights;
            supervisoryVisit.Details = model.Details;
            return await _supervisoryVisitRepository.UpdateSupervisoryVisit(supervisoryVisit);
        }

        public async Task<bool> DeleteSupervisoryVisit(int id)
        {
            var supervisoryVisit = await _supervisoryVisitRepository.GetSupervisoryVisitById(id);
            if (supervisoryVisit == null)
                return false;
            return await _supervisoryVisitRepository.DeleteSupervisoryVisit(supervisoryVisit);
        }

        public async Task<bool> AssignSupervisoryVisit(AssignSupervisoryVisitViewModel model)
        {
            var supervisoryVisit = await _supervisoryVisitRepository.GetSupervisoryVisitById(model.Id);
            if (supervisoryVisit == null)
                return false;
            supervisoryVisit.NurseId = model.NurseId;
            await _supervisoryVisitRepository.UpdateSupervisoryVisit(supervisoryVisit);
            NurseInbox message = new NurseInbox
            {
                NurseId = model.NurseId,
                EmployerId = supervisoryVisit.EmployerId,
                JobId = supervisoryVisit.JobId,
                Subject = EnumHelper.GetDescription(NurseInboxSubject.SupervisoryVisitCreated),
                SentDate = DateTime.UtcNow,
                Message = $"The Supervisory Visit for the service request ({supervisoryVisit.Job.JobTitle.Title}) for Care Recipient - {supervisoryVisit.Job.CareRecipient.FirstName} {supervisoryVisit.Job.CareRecipient.LastName} has been generated."
            };
            await _nurseInboxService.AddMessage(message);
            var nurseData = await _nurseService.GetById(model.NurseId);
            Utility.SendMailToUser(message.Subject, nurseData.EmailAddress, message.Message);
            return true;
        }

        //public Task<Response<SupervisoryVisitPDFBytes>> GetSupervisoryVisitPDFBytes(int id)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<Response<SupervisoryVisitPDFBytes>> GetSupervisoryVisitPDFBytes(int id)

        {
            try
            {
                var supervisoryVisit = _mapper.Map<SupervisoryVisitRequestModel>(await _supervisoryVisitRepository.GetById(id));
                if (supervisoryVisit == null)
                    return new Response<SupervisoryVisitPDFBytes> { Status = "Error", Message = "Please enter valid Service Agreement id" };
                var htmlString = Resource.SupervisoryVisitPDF;
                #region parameters binding


                htmlString = htmlString.Replace("[[CurrentDate]]", DateTime.Now.Date.ToString("MM/dd/yyyy"));

                htmlString = htmlString.Replace("[[PatientName]]", string.IsNullOrEmpty(supervisoryVisit.PatientName) ? "" : supervisoryVisit.PatientName);

                htmlString = htmlString.Replace("[[StaffPersonName]]", string.IsNullOrEmpty(supervisoryVisit.StaffPersonName) ? "" : supervisoryVisit.StaffPersonName);

                htmlString = htmlString.Replace("[[StaffPersonInHomeYes]]", GetCheckboxAttribute(supervisoryVisit.StaffPersonInHome, "Yes"));
                htmlString = htmlString.Replace("[[StaffPersonInHomeNo]]", GetCheckboxAttribute(supervisoryVisit.StaffPersonInHome, "No"));

                htmlString = htmlString.Replace("[SupervisioryVisitInfoCOTA]]", GetCheckboxAttribute(supervisoryVisit.SupervisoryVisitInfo, "COTA"));
                htmlString = htmlString.Replace("[[SupervisioryVisitInfoLPN]]", GetCheckboxAttribute(supervisoryVisit.SupervisoryVisitInfo, "LPN"));
                htmlString = htmlString.Replace("[[SupervisioryVisitInfoPTA]]", GetCheckboxAttribute(supervisoryVisit.SupervisoryVisitInfo, "PTA")); 
                

                htmlString = htmlString.Replace("[[FollowingPocYes]]", GetCheckboxAttribute(supervisoryVisit.FollowingPoc, "Yes"));             
                htmlString = htmlString.Replace("[FollowingPocNo]]", GetCheckboxAttribute(supervisoryVisit.FollowingPoc, "No")); 
                
                htmlString = htmlString.Replace("[[PocChangedYes]]", GetCheckboxAttribute(supervisoryVisit.PocChanged,"Yes"));
                htmlString = htmlString.Replace("[[PocChangedNo]]", GetCheckboxAttribute(supervisoryVisit.PocChanged, "No"));  


                htmlString = htmlString.Replace("[[PatientCompatibleYes]]", GetCheckboxAttribute(supervisoryVisit.PatientCompatible, "Yes")); 
                htmlString = htmlString.Replace("[[PatientCompatibleNo]]", GetCheckboxAttribute(supervisoryVisit.PatientCompatible, "No")); 
                
                htmlString = htmlString.Replace("[[CommunicatesWithPtYes]]", GetCheckboxAttribute(supervisoryVisit.CommunicatesWithPt, "Yes"));
                htmlString = htmlString.Replace("[[CommunicatesWithPtNo]]", GetCheckboxAttribute(supervisoryVisit.CommunicatesWithPt, "No")); 
                
                htmlString = htmlString.Replace("[[CompilesWithInfectionYes]]", GetCheckboxAttribute(supervisoryVisit.CompilesWithInfection, "Yes"));
                htmlString = htmlString.Replace("[[CompilesWithInfectionNo]]", GetCheckboxAttribute(supervisoryVisit.CompilesWithInfection, "No"));
                
                htmlString = htmlString.Replace("[[HonorsPatientRightsYes]]", GetCheckboxAttribute(supervisoryVisit.HonorsPatientRights, "Yes"));
                htmlString = htmlString.Replace("[[HonorsPatientRightsNo]]", GetCheckboxAttribute(supervisoryVisit.HonorsPatientRights, "No"));
                
                htmlString = htmlString.Replace("[[PatientNotifiedOfChangeNA]]", GetCheckboxAttribute(supervisoryVisit.PatientNotifiedOfChange, "NA")); 
                htmlString = htmlString.Replace("[[PatientNotifiedOfChangeYes]]", GetCheckboxAttribute(supervisoryVisit.PatientNotifiedOfChange, "Yes"));
                htmlString = htmlString.Replace("[[PatientNotifiedOfChangeNo]]", GetCheckboxAttribute(supervisoryVisit.PatientNotifiedOfChange, "No"));
                htmlString = htmlString.Replace("[[Details]]", string.IsNullOrEmpty(supervisoryVisit.Details) ? "" : supervisoryVisit.Details);


                #endregion parameters binding 


                //------------------------------------------------------------------------//

                var bytes = HtmlToByte.ConvertToPDFBytes(htmlString);
                return new Response<SupervisoryVisitPDFBytes> { Status = "Success", Data = new SupervisoryVisitPDFBytes { Bytes = bytes } };
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










        //---------------------------------------------------------------------//
        //        var bytes = HtmlToByte.ConvertToPDFBytes(htmlString);
        //        return new Response<SupervisoryVisitPDFBytes> { Status = "Success", Data = new SupervisoryVisitPDFBytes { Bytes = bytes } };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private string GetCheckboxAttribute(string staffPersonInHome)
        //{
        //    throw new NotImplementedException();
        //}

        //private string GetCheckboxAttribute(string propertyValue, string compareBy)
        //{
        //    return propertyValue == compareBy ? "checked" : "";
        //}

        //private string GetCheckboxAttribute(bool isChecked)
        //{
        //    return isChecked ? "checked" : "";
        //}
        #endregion
    }
}
