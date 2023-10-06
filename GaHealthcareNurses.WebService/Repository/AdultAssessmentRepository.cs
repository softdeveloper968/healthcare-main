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
    public class AdultAssessmentRepository : IAdultAssessmentRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For AdultAssessmentRepository
        public AdultAssessmentRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddAssessment(AdultAssessment model)
        {
            await _gaHealthcareNursesContext.AdultAssessment.AddAsync(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<AdultAssessment>> GetAssessmentByNurseId(string nurseId)
        {
            return await _gaHealthcareNursesContext.AdultAssessment.Include(x => x.Nurse).Include(x => x.Employer).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.ServiceList).Include(x => x.Job.CareRecipient.City).Where(x => x.NurseId == nurseId).ToListAsync();
        }

        public async Task<List<AdultAssessment>> GetAssessmentByEmployerId(string employerId)
        {
            return await _gaHealthcareNursesContext.AdultAssessment.Include(x => x.Nurse).Include(x => x.Employer).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.ServiceList).Include(x => x.Job.CareRecipient.City).Where(x => x.EmployerId == employerId).ToListAsync();
        }

        public async Task<AdultAssessment> GetAssessmentById(int id)
        {
            return await _gaHealthcareNursesContext.AdultAssessment.Include(x => x.Employer).ThenInclude(x => x.State).Include(x => x.Employer.City).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.City).Include(x => x.Job.JobTitle).Include(x => x.Diagnosis).Include(x => x.Wound).Where(x => x.AdultAssessmentId == id).FirstOrDefaultAsync();
        }

        public async Task<AdultAssessment> GetAssessmentByJobId(int jobId)
        {
            return await _gaHealthcareNursesContext.AdultAssessment.Include(x => x.Employer).ThenInclude(x => x.State).Include(x => x.Employer.City).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.City).Include(x => x.Job.JobTitle).Include(x => x.Diagnosis).Include(x => x.Wound).Where(x => x.JobId == jobId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAssessment(AdultAssessment model)
        {
            _gaHealthcareNursesContext.AdultAssessment.Update(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAssessment(AdultAssessment model)
        {
            _gaHealthcareNursesContext.AdultAssessment.Remove(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
