using Contracts;
using GaHealthcareNurses.Entity;
using GaHealthcareNurses.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class NotifyConfigurationRepository : INotifyConfigurationRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For AgencyServedCountiesRepository
        public NotifyConfigurationRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddConfiguration(NotifyConfiguration notifyConfiguration)
        {
            await _gaHealthcareNursesContext.NotifyConfiguration.AddAsync(notifyConfiguration);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<NotifyConfiguration> GetByEmployerId(string employerId)
        {
            return await _gaHealthcareNursesContext.NotifyConfiguration.FirstOrDefaultAsync(x => x.EmployerId == employerId);
        }

        public async Task<NotifyConfiguration> GetById(int id)
        {
            return await _gaHealthcareNursesContext.NotifyConfiguration.FirstOrDefaultAsync(x => x.NotifyConfigurationId == id);
        }

        public async Task<bool> UpdateConfiguration(NotifyConfiguration notifyConfiguration)
        {
            _gaHealthcareNursesContext.NotifyConfiguration.Update(notifyConfiguration);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
