using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface IDischargeSummaryService
    {
        Task<bool> CreateDischargeSummary(DischargeSummary model);
        Task<string> CompleteServiceRequestFromMobile(CompleteServiceRequestFromMobileRequestModel model);
        Task<List<GetDischargeSummaryListResponseModel>> GetAllDischargeSummaries();
        Task<List<GetDischargeSummaryListResponseModel>> GetDischargeSummariesByNurseId(string nurseId);
        Task<List<GetDischargeSummaryListResponseModel>> GetDischargeSummariesByEmployerId(string employerId);
        Task<DischargeSummaryRequestModel> GetDischargeSummariesById(int id);
        Task<Response<DischargeSummaryPDFBytes>> GetDischargeSummaryPDFBytes(int id);
        Task<bool> CompleteDischargeSummary(DischargeSummaryRequestModel model);
        Task<string> AcceptOrRejectDischargeSummary(AcceptOrRejectDischargeSummaryRequestModel model);
        Task<bool> DeleteDischargeSummary(int id);
        Task<bool> AssignDischargeSummary(AssignDischargeSummaryViewModel model);
    }
}
