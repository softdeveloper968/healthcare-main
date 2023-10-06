using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAdultAssessmentRepository
    {
        Task<bool> AddAssessment(AdultAssessment model);
        Task<List<AdultAssessment>> GetAssessmentByNurseId(string nurseId);
        Task<List<AdultAssessment>> GetAssessmentByEmployerId(string employerId);
        Task<AdultAssessment> GetAssessmentById(int id);
        Task<AdultAssessment> GetAssessmentByJobId(int jobId);
        Task<bool> UpdateAssessment(AdultAssessment model);
        Task<bool> DeleteAssessment(AdultAssessment model);
    }
}
