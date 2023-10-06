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
    public class CarePlanRepository : ICarePlanRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;

        #region Contructor For CarePlanRepository
        public CarePlanRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddCarePlan(CarePlan model)
        {
            await _gaHealthcareNursesContext.CarePlan.AddAsync(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<CarePlan>> GetCarePlanByNurseId(string nurseId)
        {
            return await _gaHealthcareNursesContext.CarePlan.Include(x => x.Nurse).Include(x => x.Employer).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.ServiceList).Include(x => x.Job.CareRecipient.City).Where(x => x.NurseId == nurseId).ToListAsync();
        }

        public async Task<List<CarePlan>> GetCarePlanByEmployerId(string employerId)
        {
            return await _gaHealthcareNursesContext.CarePlan.Include(x => x.Nurse).Include(x => x.Employer).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.ServiceList).Include(x => x.Job.CareRecipient.City).Where(x => x.EmployerId == employerId).ToListAsync();
        }

        public async Task<CarePlan> GetCarePlanById(int id)
        {
            return await _gaHealthcareNursesContext.CarePlan.Include(x => x.Employer).ThenInclude(x => x.State).Include(x => x.Employer.City).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.City).Include(x => x.Job.JobTitle).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<CarePlan> GetCarePlanByJobId(int JobId)
        {
            return await _gaHealthcareNursesContext.CarePlan.Include(x => x.Employer).ThenInclude(x => x.State).Include(x => x.Employer.City).Include(x => x.Job).ThenInclude(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.Job.CareRecipient.City).Include(x => x.Job.JobTitle).Where(x => x.JobId == JobId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateCarePlan(CarePlan model)
        {
            _gaHealthcareNursesContext.CarePlan.Update(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCarePlan(CarePlan model)
        {
            _gaHealthcareNursesContext.CarePlan.Remove(model);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
