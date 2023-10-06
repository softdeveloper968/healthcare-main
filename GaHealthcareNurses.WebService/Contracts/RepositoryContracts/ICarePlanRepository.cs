using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICarePlanRepository
    {
        Task<bool> AddCarePlan(CarePlan model);
        Task<List<CarePlan>> GetCarePlanByNurseId(string nurseId);
        Task<List<CarePlan>> GetCarePlanByEmployerId(string employerId);
        Task<CarePlan> GetCarePlanById(int id);
        Task<CarePlan> GetCarePlanByJobId(int jobId);
        Task<bool> UpdateCarePlan(CarePlan model);
        Task<bool> DeleteCarePlan(CarePlan model);
    }
}
