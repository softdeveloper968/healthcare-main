using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface INotifyConfigurationRepository
    {
        Task<NotifyConfiguration> GetById(int id);
        Task<NotifyConfiguration> GetByEmployerId(string employerId);
        Task<bool> AddConfiguration(NotifyConfiguration taskListCategory);
        Task<bool> UpdateConfiguration(NotifyConfiguration taskListCategory);
    }
}
