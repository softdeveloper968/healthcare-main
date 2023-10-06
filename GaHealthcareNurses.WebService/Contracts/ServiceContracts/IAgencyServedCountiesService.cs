using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface IAgencyServedCountiesService
    {
        Task AddServedCounty(string employerId, List<int> counties);
        Task UpdateServedCounties(string employerId, List<int> counties);
        Task<List<AgencyServedCounties>> GetServedCountiesByCountyId(int countyId);
        Task<List<AgencyServedCounties>> GetServedCountiesByEmployerId(string employerId);
    }
}
