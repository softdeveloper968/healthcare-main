using Contracts;
using GaHealthcareNurses.Entity;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SupervisoryVisitRepository : ISupervisoryVisitRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For SupervisoryVisitRepository
        public SupervisoryVisitRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddSupervisoryVisit(SupervisoryVisit model)
        {
            await _gaHealthcareNursesContext.SupervisoryVisit.AddAsync(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<SupervisoryVisit>> GetSupervisoryVisitByNurseId(string nurseId)
        {
            return await _gaHealthcareNursesContext.SupervisoryVisit.Include(x => x.Nurse).Include(x => x.Employer).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.ServiceList).Include(x => x.Job.CareRecipient.City).Where(x => x.NurseId == nurseId).ToListAsync();
        }

        public async Task<List<SupervisoryVisit>> GetSupervisoryVisitByEmployerId(string employerId)
        {
            return await _gaHealthcareNursesContext.SupervisoryVisit.Include(x => x.Nurse).Include(x => x.Employer).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.ServiceList).Include(x => x.Job.CareRecipient.City).Where(x => x.EmployerId == employerId).ToListAsync();
        }

        public async Task<SupervisoryVisit> GetSupervisoryVisitById(int id)
        {
            return await _gaHealthcareNursesContext.SupervisoryVisit.Include(x => x.Job).ThenInclude(x => x.CareRecipient).Include(x => x.Job.JobTitle).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateSupervisoryVisit(SupervisoryVisit model)
        {
            _gaHealthcareNursesContext.SupervisoryVisit.Update(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSupervisoryVisit(SupervisoryVisit model)
        {
            _gaHealthcareNursesContext.SupervisoryVisit.Remove(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }



        public async Task<SupervisoryVisit> GetById(int id)
        {
            return await _gaHealthcareNursesContext.SupervisoryVisit.Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.ServiceList).Include(x => x.Employer).Include(x => x.Job.JobApplyForAgencies.Where(y => y.StatusId == 13)).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        #endregion
    }
}
