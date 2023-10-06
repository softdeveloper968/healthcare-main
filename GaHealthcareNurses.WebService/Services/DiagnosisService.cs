using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DiagnosisService : IDiagnosisService
    {
        private IDiagnosisRepository _diagnosisRepository;

        #region Contructor For DiagnosisService
        public DiagnosisService(IDiagnosisRepository diagnosisRepository)
        {
            _diagnosisRepository = diagnosisRepository;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddDiagnosis(List<Diagnosis> diagnoses)
        {
            return await _diagnosisRepository.AddDiagnosis(diagnoses);
        }

        public async Task<bool> DeleteDiagnosis(List<Diagnosis> diagnoses)
        {
            return await _diagnosisRepository.DeleteDiagnosis(diagnoses);
        }

        public async Task<List<Diagnosis>> GetByAdultAssessmentId(int adultAssessmentId)
        {
            return await _diagnosisRepository.GetByAdultAssessmentId(adultAssessmentId);
        }
        #endregion
    }
}
