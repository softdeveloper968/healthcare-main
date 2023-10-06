using Contracts.RepositoryContracts;
using GaHealthcareNurses.Entity;
using GaHealthcareNurses.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ServiceAgreementRepository : IServiceAgreementRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For ServiceAgreementRepository
        public ServiceAgreementRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddServiceAgreement(ServiceAgreement model)
        {
            await _gaHealthcareNursesContext.ServiceAgreement.AddAsync(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<ServiceAgreement>> GetByEmployerId(string employerId)
        {
            return await _gaHealthcareNursesContext.ServiceAgreement.Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.ServiceList).Where(x => x.EmployerId == employerId && !x.IsArchived).ToListAsync();
        }

        public async Task<ServiceAgreement> GetById(int id)
        {
            return await _gaHealthcareNursesContext.ServiceAgreement.Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.ServiceList).Include(x => x.Job.JobTitle).Include(x => x.Employer).Include(x => x.Job.JobApplyForAgencies.Where(y => y.StatusId == 13)).Where(x => x.ServiceAgreementId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateServiceAgreement(ServiceAgreement model)
        {
            _gaHealthcareNursesContext.ServiceAgreement.Update(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
