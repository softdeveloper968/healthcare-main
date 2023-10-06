﻿using Contracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GoogleMaps.LocationServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using Services.Helper;
using GaHealthcareNurses.Entity.ViewModels;
using GaHealthcareNurses.Entity.Common;

namespace Services
{
    public class CareRecipientService : ICareRecipientService
    {
        private ICareRecipientRepository _careRecipientRepository;
        private ICityService _cityService;
        private IStateService _stateService;
        private IServiceListService _serviceListService;

        #region Constructor for CareRecipientService
        public CareRecipientService(ICareRecipientRepository careRecipientRepository, ICityService cityService, IStateService stateService, IServiceListService serviceListService)
        {
            _careRecipientRepository = careRecipientRepository;
            _cityService = cityService;
            _stateService = stateService;
            _serviceListService = serviceListService;
        }
        #endregion

        #region Implementing Interface
        public async Task<IEnumerable<CareRecipient>> Get()
        {
            return await _careRecipientRepository.Get();
        }

        public async Task<CareRecipient> GetById(int id)
        {
            return await _careRecipientRepository.GetById(id);
        }

        public async Task<int> Add(CareRecipient careRecipient)
        {
            var city = await _cityService.GetById((int)careRecipient.CityId);
            var state = await _stateService.GetById((int)careRecipient.StateId);
            var latitudeAndLongitude = GetLatitudeAndLongitude.GetLatLogFromAddress(careRecipient.AddressLine1, city.Name, state.Name, city.ZipCode);
            if (latitudeAndLongitude != null)
            {
                careRecipient.Latitude = latitudeAndLongitude.Latitude;
                careRecipient.Longitude = latitudeAndLongitude.Longitude;
            }
            var service = await _serviceListService.GetById(careRecipient.ServiceListId);
            careRecipient.ServiceList = service;

            return await _careRecipientRepository.Add(careRecipient);
        }

        public async Task Delete(CareRecipient careRecipient)
        {
            await _careRecipientRepository.Delete(careRecipient);
        }

        public async Task Update(CareRecipient careRecipient)
        {
            await _careRecipientRepository.Update(careRecipient);
        }

        public async Task<bool> UpdateStatusAndVisibility(UpdateStatusVisibilityViewModel model)
        {
            var careRecipient = await _careRecipientRepository.GetById(model.CareRecipientId);
            if (careRecipient == null)
                return false;
            careRecipient.Status = model.Status;
            careRecipient.Visibility = model.Visibility;
            await _careRecipientRepository.Update(careRecipient);
            return true;
        }

        public async Task SendCEUEmail(CEUViewModel cEUViewModel)
        {
            string templatePath = Environment.CurrentDirectory + @"\\EmailTemplates\CEUTemplate.xml";
            var sendEmail = SendCEUMail(cEUViewModel, templatePath);
        }

        public async Task<bool> SendCEUMail(CEUViewModel cEUViewModel, string templatePath)
        {
            try
            {
                string sendTo = "HR@PARTNERSHIP4HEALTH.COM";
                string emailBody = string.Empty;
                emailBody = Utility.GetEmailTemplateValue(templatePath, "NeedCEUEmail/Body");
                emailBody = emailBody.Replace("@@@firstname", cEUViewModel.FirstName);
                emailBody = emailBody.Replace("@@@lastname", cEUViewModel.LastName);
                emailBody = emailBody.Replace("@@@email", cEUViewModel.Email);
                emailBody = emailBody.Replace("@@@state", cEUViewModel.State);
                emailBody = emailBody.Replace("@@@qualification", cEUViewModel.Qualification);
                Utility.SendMailToUser(EnumHelper.GetDescription(EmailSubjectRole.CEU), sendTo, emailBody);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
