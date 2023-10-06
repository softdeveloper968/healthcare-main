using Contracts.RepositoryContracts;
using GaHealthcareNurses.Entity;
using GaHealthcareNurses.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DiagnosisRepository : IDiagnosisRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For DiagnosisRepository
        public DiagnosisRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddDiagnosis(List<Diagnosis> diagnoses)
        {
            await _gaHealthcareNursesContext.Diagnosis.AddRangeAsync(diagnoses);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDiagnosis(List<Diagnosis> diagnoses)
        {
            _gaHealthcareNursesContext.Diagnosis.RemoveRange(diagnoses);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Diagnosis>> GetByAdultAssessmentId(int adultAssessmentId)
        {
            return await _gaHealthcareNursesContext.Diagnosis.Where(x => x.AdultAssessmentId == adultAssessmentId).ToListAsync();
        }
        #endregion
    }
}
