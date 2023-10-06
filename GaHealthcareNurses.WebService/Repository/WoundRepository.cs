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
    public class WoundRepository : IWoundRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For WoundRepository
        public WoundRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddWounds(List<Wound> wounds)
        {
            await _gaHealthcareNursesContext.Wound.AddRangeAsync(wounds);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWounds(List<Wound> wounds)
        {
            _gaHealthcareNursesContext.Wound.RemoveRange(wounds);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Wound>> GetByAdultAssessmentId(int adultAssessmentId)
        {
            return await _gaHealthcareNursesContext.Wound.Where(x => x.AdultAssessmentId == adultAssessmentId).ToListAsync();
        }
        #endregion
    }
}
