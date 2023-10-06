using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface IInOutTimeService 
    {
        Task<bool> ClockIn(ClockInViewModel model);
        Task<bool> ClockOut(ClockOutViewModel model);
    }
}
