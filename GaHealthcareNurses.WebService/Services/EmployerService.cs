using AutoMapper;
using Contracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using Repository;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployerService : IEmployerService
    {
        private IEmployerRepository _employerRepository;
        private IJobService _jobService;
        private IAgencyServedCountiesService _agencyServedCountiesService;
        private IMapper _mapper;

        #region Constructor for EmployerService
        public EmployerService(IEmployerRepository employerRepository, IJobService jobService, IAgencyServedCountiesService agencyServedCountiesService, IMapper mapper)
        {
            _employerRepository = employerRepository;
            _jobService = jobService;
            _agencyServedCountiesService = agencyServedCountiesService;
            _mapper = mapper;
        }
        #endregion

        #region Implementing Interface
        public async Task<IEnumerable<Employer>> Get(int skip, int take, string filter)
        {
            return await _employerRepository.Get(skip, take, filter);
        }

        public async Task<IEnumerable<Employer>> GetAll()
        {
            return await _employerRepository.GetAll();
        }

        public async Task<int> TotalCount(string filter)
        {
            return await _employerRepository.TotalCount(filter);
        }

        public async Task<Employer> GetById(string id)
        {
            return await _employerRepository.GetById(id);
        }

        public async Task<bool> GetByName(Employer employer)
        {
            return await _employerRepository.GetByName(employer);
        }

        public async Task Add(Employer employer)
        {
            await _employerRepository.Add(employer);
        }

        public async Task<bool> Delete(string id)
        {
            var employer = await GetById(id);
            if (employer == null)
                return false;
            employer.IsDeleted = true;
            await _employerRepository.Update(employer);
            return true;
        }

        public async Task<Employer> Update(EmployerViewModel employer)
        {
            var employerData = await _employerRepository.GetById(employer.EmployerId);
            employerData.Name = employer.Name;
            employerData.AddressLine1 = employer.AddressLine1;
            employerData.AddressLine2 = employer.AddressLine2;
            employerData.CityId = employer.CityId;
            employerData.StateId = employer.StateId;
            employerData.ZipCode = employer.ZipCode;
            employerData.TelephoneNo = employer.TelephoneNo;
            employerData.EmailAddress = employer.EmailAddress;
            employerData.AgencyWebsite = employer.AgencyWebsite;
            employerData.LPNs = employer.LPNs;
            employerData.PCAs = employer.PCAs;
            employerData.RNs = employer.RNs;
            employerData.CNAs = employer.CNAs;

            if (employer.Logo != null)
            {
                var logoPath = UploadAndDownloadFileAzure.UploadDocument(employer.Logo, employerData.Id);
                employerData.Logo = logoPath;
            }
            var servedCounties = employer.ServedCounties.Split(',').Select(int.Parse).ToList();
            await _agencyServedCountiesService.UpdateServedCounties(employerData.Id, servedCounties);
            return await _employerRepository.Update(employerData);
        }

        public async Task<List<Employer>> GetAgenciesForBid(int jobId)
        {
            var job = await _jobService.GetById(jobId);
            if (job != null && job.CareRecipient.City.CountyId != null)
            {
                var counties = await _agencyServedCountiesService.GetServedCountiesByCountyId(Convert.ToInt32(job.CareRecipient.City.CountyId));
                return counties.Select(x => x.Employer).ToList();
            }
            return new List<Employer>();
        }

        public async Task<List<AgencyServedCounties>> GetAgencyServedCounties(string employerId)
        {
            return await _agencyServedCountiesService.GetServedCountiesByEmployerId(employerId);
        }

        public async Task<bool> SetPermissions(SetPermissionsViewModel model)
        {
            var employer = await _employerRepository.GetById(model.EmployerId);
            if (employer == null)
                return false;
            employer.NursesList = model.NursesList;
            employer.Tasks = model.Tasks;
            employer.Documents = model.Documents;
            employer.HeatMap = model.HeatMap;
            employer.SupervisoryVisits = model.SupervisoryVisits;
            employer.InitialAssessment = model.InitialAssessment;
            employer.DischargeSummary = model.DischargeSummary;
            employer.CarePlan = model.CarePlan;
            employer.CareRecipients = model.CareRecipients;
            employer.ServiceAgreements = model.ServiceAgreements;
            employer.Calendar = model.Calendar; 
            await _employerRepository.Update(employer);
            return true;
        }

        public async Task<List<Nurse>> GetNursesList(string employerId)
        {
            return await _employerRepository.GetNursesList(employerId);
        }
        #endregion
    }
}
