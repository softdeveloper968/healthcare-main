using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IAgencyServedCountiesRepository
    {
        Task AddServedCounty(AgencyServedCounties model);
        Task UpdateServedCounty(AgencyServedCounties model);
        Task DeleteServedCounties(List<AgencyServedCounties> agencyServedCounties);
        Task<List<AgencyServedCounties>> GetServedCountiesByCountyId(int countyId);
        Task<List<AgencyServedCounties>> GetServedCountiesByEmployerId(string employerId);
    }
}
