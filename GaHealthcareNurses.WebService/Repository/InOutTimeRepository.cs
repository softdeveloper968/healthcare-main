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
    public class InOutTimeRepository : IInOutTimeRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For ServiceAgreementRepository
        public InOutTimeRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddInOutTime(InOutTime model)
        {
            await _gaHealthcareNursesContext.InOutTime.AddAsync(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<InOutTime> GetInOutTimeById(int id)
        {
            return await _gaHealthcareNursesContext.InOutTime.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<InOutTime> GetInOutTimeForDay(DateTime date, int jobId)
        {
            return await _gaHealthcareNursesContext.InOutTime.FirstOrDefaultAsync(x => x.JobId == jobId && x.ClockInTime.Value.Date == date.Date);
        }

        public async Task<bool> UpdateInOutTime(InOutTime model)
        {
            _gaHealthcareNursesContext.InOutTime.Update(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
