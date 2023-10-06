using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IDiagnosisRepository
    {
        Task<List<Diagnosis>> GetByAdultAssessmentId(int adultAssessmentId);
        Task<bool> AddDiagnosis(List<Diagnosis> diagnoses);
        Task<bool> DeleteDiagnosis(List<Diagnosis> diagnoses);
    }
}
