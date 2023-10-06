using Contracts.RepositoryContracts;
using GaHealthcareNurses.Entity;
using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AgencyServedCountiesRepository : IAgencyServedCountiesRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For AgencyServedCountiesRepository
        public AgencyServedCountiesRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task AddServedCounty(AgencyServedCounties model)
        {
            await _gaHealthcareNursesContext.AgencyServedCounties.AddAsync(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
        }

        public async Task UpdateServedCounty(AgencyServedCounties model)
        {
            _gaHealthcareNursesContext.AgencyServedCounties.Update(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
        }

        public async  Task DeleteServedCounties(List<AgencyServedCounties> agencyServedCounties)
        {
            _gaHealthcareNursesContext.AgencyServedCounties.RemoveRange(agencyServedCounties);
            await _gaHealthcareNursesContext.SaveChangesAsync();
        }

        public async Task<List<AgencyServedCounties>> GetServedCountiesByCountyId(int countyId)
        {
            return await _gaHealthcareNursesContext.AgencyServedCounties.Include(x => x.Employer).Where(x => x.CountyId == countyId && !x.Employer.IsDeleted).ToListAsync();
        }

        public async Task<List<AgencyServedCounties>> GetServedCountiesByEmployerId(string employerId)
        {
            return await _gaHealthcareNursesContext.AgencyServedCounties.Where(x => x.EmployerId == employerId).ToListAsync();
        }
        #endregion
    }
}
