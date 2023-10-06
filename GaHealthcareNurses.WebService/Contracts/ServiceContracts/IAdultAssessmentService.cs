using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface IAdultAssessmentService
    {
        Task<bool> CreateAssessment(AdultAssessment model);
        Task<List<GetAdultAssessmentListResponseModel>> GetAssessmentByNurseId(string nurseId);
        Task<List<GetAdultAssessmentListResponseModel>> GetAssessmentByEmployerId(string employerId);
        Task<AdultAssessmentRequestModel> GetAssessmentById(int id);
        Task<AdultAssessmentRequestModel> GetAssessmentByJobId(int jobId);
        Task<bool> UpdateAssessment(AdultAssessmentRequestModel model);
        Task<bool> DeleteAssessment(int id);
        Task<bool> AssignAssessment(AssignAdultAssessmentViewModel model);
    }
}
