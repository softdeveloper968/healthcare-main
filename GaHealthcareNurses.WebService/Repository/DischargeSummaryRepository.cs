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
    public class DischargeSummaryRepository : IDischargeSummaryRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For DischargeSummaryRepository
        public DischargeSummaryRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddDischargeSummary(DischargeSummary model)
        {
            await _gaHealthcareNursesContext.DischargeSummary.AddAsync(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<DischargeSummary>> GetDischargeSummariesByNurseId(string nurseId)
        {
            return await _gaHealthcareNursesContext.DischargeSummary.Include(x => x.Nurse).Include(x => x.Employer).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.ServiceList).Include(x => x.Job.CareRecipient.City).Where(x => x.NurseId == nurseId).ToListAsync();
        }

        public async Task<List<DischargeSummary>> GetDischargeSummariesByEmployerId(string employerId)
        {
            return await _gaHealthcareNursesContext.DischargeSummary.Include(x => x.Nurse).Include(x => x.Employer).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.ServiceList).Include(x => x.Job.CareRecipient.City).Where(x => x.EmployerId == employerId).ToListAsync();
        }

        public async Task<DischargeSummary> GetDischargeSummariesById(int id)
        {
            return await _gaHealthcareNursesContext.DischargeSummary.Include(x => x.Job).ThenInclude(x => x.CareRecipient).Include(x => x.Job.JobTitle).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateDischargeSummary(DischargeSummary model)
        {
            _gaHealthcareNursesContext.DischargeSummary.Update(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDischargeSummary(DischargeSummary dischargeSummary)
        {
            _gaHealthcareNursesContext.Remove(dischargeSummary);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<DischargeSummary>> GetAllDischargeSummaries()
        {
            return await _gaHealthcareNursesContext.DischargeSummary.Include(x => x.Nurse).Include(x => x.Employer).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.ServiceList).Include(x => x.Job.CareRecipient.City).ToListAsync();
        }

        public async Task<DischargeSummary> GetDischargeSummaryByJobId(int jobId)
        {
            return await _gaHealthcareNursesContext.DischargeSummary.Where(x => x.JobId == jobId).FirstOrDefaultAsync();
        }
        #endregion
    }
}
