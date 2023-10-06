using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface ICarePlanService
    {
        Task<bool> CreatePlan(CarePlan model);
        Task<List<GetCarePlanListResponseModel>> GetCarePlanByNurseId(string nurseId);
        Task<List<GetCarePlanListResponseModel>> GetCarePlanByEmployerId(string employerId);
        Task<CarePlanRequestModel> GetCarePlanById(int id);
        Task<CarePlanRequestModel> GetCarePlanByJobId(int jobId);
        Task<bool> UpdateCarePlan(CarePlanRequestModel model);
        Task<bool> DeleteCarePlan(int id);
        Task<bool> AssignCarePlan(AssignCarePlanViewModel model);
        Task<Response<CarePlanPDFBytes>> GetCarePlanPDFBytes(int id);
    }
}
