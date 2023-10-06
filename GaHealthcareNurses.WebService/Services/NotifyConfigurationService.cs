using AutoMapper;
using Contracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NotifyConfigurationService : INotifyConfigurationService
    {
        private INotifyConfigurationRepository _notifyConfigurationRepository;
        private IEmployerService _employerService;
        private IMapper _mapper;

        #region Constructor for NotifyConfigurationService
        public NotifyConfigurationService(INotifyConfigurationRepository notifyConfigurationRepository, IEmployerService employerService, IMapper mapper)
        {
            _notifyConfigurationRepository = notifyConfigurationRepository;
            _employerService = employerService;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddUpdateConfiguration(NotifyConfigurationRequestModel model)
        {
            var employer = await _employerService.GetById(model.EmployerId);
            if (employer == null)
                return false;
            var configuration = await _notifyConfigurationRepository.GetByEmployerId(model.EmployerId);
            if (configuration == null)
            {
                return await _notifyConfigurationRepository.AddConfiguration(new NotifyConfiguration
                {
                    EmployerId = model.EmployerId,
                    DaysBeforeDocExpires = Convert.ToInt32(model.DaysBeforeDocExpires),
                    HoursFrequencyForDocs = Convert.ToDouble(model.HoursFrequencyForDocs),
                    NotifyDocExpByApp = model.NotifyDocExpByApp,
                    NotifyDocExpByText = model.NotifyDocExpByText,
                    NotifyDocExpByEmail = model.NotifyDocExpByEmail,
                    MinutesBeforeClockIn = Convert.ToInt32(model.MinutesBeforeClockIn),
                    MinutesFreqToRepeat = Convert.ToInt32(model.MinutesFreqToRepeat),
                    MinutesAfterShiftEnd = Convert.ToInt32(model.MinutesAfterShiftEnd)
                });
            }

            configuration.DaysBeforeDocExpires = Convert.ToInt32(model.DaysBeforeDocExpires);
            configuration.HoursFrequencyForDocs = Convert.ToDouble(model.HoursFrequencyForDocs);
            configuration.NotifyDocExpByApp = model.NotifyDocExpByApp;
            configuration.NotifyDocExpByText = model.NotifyDocExpByText;
            configuration.NotifyDocExpByEmail = model.NotifyDocExpByEmail;
            configuration.MinutesBeforeClockIn = Convert.ToInt32(model.MinutesBeforeClockIn);
            configuration.MinutesFreqToRepeat = Convert.ToInt32(model.MinutesFreqToRepeat);
            configuration.MinutesAfterShiftEnd = Convert.ToInt32(model.MinutesAfterShiftEnd);
            return await _notifyConfigurationRepository.UpdateConfiguration(configuration);
        }

        public async Task<NotifyConfigurationRequestModel> GetByEmployerId(string employerId)
        {
            return _mapper.Map<NotifyConfigurationRequestModel>(await _notifyConfigurationRepository.GetByEmployerId(employerId));
        }
        #endregion
    }
}
