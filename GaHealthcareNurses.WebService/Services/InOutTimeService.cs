using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InOutTimeService : IInOutTimeService
    {
        private IInOutTimeRepository _inOutTimeRepository;

        #region Constructor for InOutTimeService
        public InOutTimeService(IInOutTimeRepository inOutTimeRepository)
        {
            _inOutTimeRepository = inOutTimeRepository;
        }
        #endregion
        #region Implementing Interface
        public async Task<bool> ClockIn(ClockInViewModel model)
        {
            var clockInTime = await _inOutTimeRepository.GetInOutTimeForDay(model.ClockInTime, model.JobId);
            if (clockInTime != null)
                return false;
            return await _inOutTimeRepository.AddInOutTime(new InOutTime
            {
                NurseId = model.NurseId,
                JobId = model.JobId,
                ClockInTime = model.ClockInTime,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            });
        }

        public async Task<bool> ClockOut(ClockOutViewModel model)
        {
            var clockOutTime = await _inOutTimeRepository.GetInOutTimeForDay(model.ClockOutTime, model.JobId);
            if (clockOutTime == null)
                return false;
            clockOutTime.Latitude = model.Latitude;
            clockOutTime.Longitude = model.Longitude;
            clockOutTime.ClockOutTime = model.ClockOutTime;
           return await _inOutTimeRepository.UpdateInOutTime(clockOutTime);
        }

        #endregion
    }
}
