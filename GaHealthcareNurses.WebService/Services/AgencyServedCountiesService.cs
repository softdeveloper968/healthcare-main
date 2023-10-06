using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AgencyServedCountiesService : IAgencyServedCountiesService
    {
        private IAgencyServedCountiesRepository _agencyServedCountiesRepository;

        #region Contructor For AgencyServedCountiesService
        public AgencyServedCountiesService(IAgencyServedCountiesRepository agencyServedCountiesRepository)
        {
            _agencyServedCountiesRepository = agencyServedCountiesRepository;
        }
        #endregion
        public async Task AddServedCounty(string employerId, List<int> counties)
        {
            if(counties != null && counties.Count > 0)
            {
                foreach(var county in counties)
                {
                    await _agencyServedCountiesRepository.AddServedCounty(new AgencyServedCounties
                    {
                        CountyId = county,
                        EmployerId = employerId
                    });
                }
            }
        }

        public async Task UpdateServedCounties(string employerId, List<int> counties)
        {
            var existingCounties = await _agencyServedCountiesRepository.GetServedCountiesByEmployerId(employerId);
            if(existingCounties.Count > 0)
            {
                await _agencyServedCountiesRepository.DeleteServedCounties(existingCounties);
            }
            await AddServedCounty(employerId, counties);
        }

        public async Task<List<AgencyServedCounties>> GetServedCountiesByCountyId(int countyId)
        {
            return await _agencyServedCountiesRepository.GetServedCountiesByCountyId(countyId);
        }

        public async Task<List<AgencyServedCounties>> GetServedCountiesByEmployerId(string employerId)
        {
            return await _agencyServedCountiesRepository.GetServedCountiesByEmployerId(employerId);
        }
    }
}
