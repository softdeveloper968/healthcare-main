using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface ISupervisoryVisitService
    {
        Task<bool> CreateSupervisoryVisit(SupervisoryVisit model);
        Task<List<GetSupervisoryVisitListResponseModel>> GetSupervisoryVisitByNurseId(string nurseId);
        Task<List<GetSupervisoryVisitListResponseModel>> GetSupervisoryVisitByEmployerId(string employerId);
        Task<SupervisoryVisitRequestModel> GetSupervisoryVisitById(int id);
        Task<bool> UpdateSupervisoryVisit(SupervisoryVisitRequestModel model);
        Task<bool> DeleteSupervisoryVisit(int id);
        Task<bool> AssignSupervisoryVisit(AssignSupervisoryVisitViewModel model);
        //Task<object> GetServiceAgreementPDFBytes(int id);
        Task<Response<SupervisoryVisitPDFBytes>> GetSupervisoryVisitPDFBytes(int id);
    }
}
