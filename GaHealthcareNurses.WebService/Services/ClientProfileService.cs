using Contracts;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using iText.Html2pdf.Attach;
using Repository;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClientProfileService : IClientProfileService
    {
        private IClientProfileRepository _clientProfileRepository;

        #region Constructor for EmployerService
        public ClientProfileService(IClientProfileRepository clientProfileRepository)
        {
            _clientProfileRepository = clientProfileRepository;
        }
        #endregion

        #region Implementing Interface
        public async Task Add(ClientProfile clientProfile)
        {
            await _clientProfileRepository.Add(clientProfile);
        }

        public async Task<ClientProfile> Update(ClientProfile clientProfile)
        {
            ClientProfile _clientProfile = await _clientProfileRepository.GetById(Convert.ToString(clientProfile.Id));

            _clientProfile.Title = clientProfile.Title;
            _clientProfile.FirstName = clientProfile.FirstName;
            _clientProfile.LastName = clientProfile.LastName;
            _clientProfile.GoesBy = clientProfile.GoesBy;
            _clientProfile.Gender = clientProfile.Gender;
            _clientProfile.DateOfBirth = clientProfile.DateOfBirth;
            _clientProfile.SSN = clientProfile.SSN;
            _clientProfile.StatusId = clientProfile.StatusId;
            _clientProfile.LeadCreatedDate = clientProfile.LeadCreatedDate;
            _clientProfile.InitAssessmentDate = clientProfile.InitAssessmentDate;
            _clientProfile.ConversionDate = clientProfile.ConversionDate;
            _clientProfile.StartDate = clientProfile.StartDate;
            _clientProfile.EffectiveEndDate = clientProfile.EffectiveEndDate;
            _clientProfile.MailingName = clientProfile.MailingName;
            _clientProfile.Address = clientProfile.Address;
            _clientProfile.City = clientProfile.City;
            _clientProfile.StateId = clientProfile.StateId;
            _clientProfile.ZipCode = clientProfile.ZipCode;
            _clientProfile.Geocode = clientProfile.Geocode;
            _clientProfile.MobAppGeofence = clientProfile.MobAppGeofence;
            _clientProfile.OverrideGeofenceRadiusMet = clientProfile.OverrideGeofenceRadiusMet;
            _clientProfile.Weight = clientProfile.Weight;
            _clientProfile.HeightInFeet = clientProfile.HeightInFeet;
            _clientProfile.HeightInInch = clientProfile.HeightInInch;
            _clientProfile.LanguageId = clientProfile.LanguageId;
            _clientProfile.Occupation = clientProfile.Occupation;
            _clientProfile.JobTitle = clientProfile.JobTitle;
            _clientProfile.Hobbies = clientProfile.Hobbies;
            _clientProfile.TriageLevelId = clientProfile.TriageLevelId;
            _clientProfile.AdvanceDir = clientProfile.AdvanceDir;
            _clientProfile.Allergies = clientProfile.Allergies;
            _clientProfile.DNR = clientProfile.DNR;
            _clientProfile.Will = clientProfile.Will;
            _clientProfile.PetOne = clientProfile.PetOne;
            _clientProfile.PetTwo = clientProfile.PetTwo;
            _clientProfile.PetThree = clientProfile.PetThree;
            _clientProfile.PhoneHome = clientProfile.PhoneHome;
            _clientProfile.PhoneMobile = clientProfile.PhoneMobile;
            _clientProfile.PhoneOther = clientProfile.PhoneOther;
            _clientProfile.Mobile = clientProfile.Mobile;
            _clientProfile.TelephonyPhone = clientProfile.TelephonyPhone;
            _clientProfile.EmailAddr = clientProfile.EmailAddr;

            return await _clientProfileRepository.Update(_clientProfile);
        }

        public async Task<IEnumerable<ClientProfile>> Get()
        {
            return await _clientProfileRepository.Get();
        }

        public async Task<ClientProfile> GetById(string id)
        {
            return await _clientProfileRepository.GetById(id);
        }

        public async Task Delete(ClientProfile clientProfile)
        {
            await _clientProfileRepository.Delete(clientProfile);
        }
        #endregion
    }
}
