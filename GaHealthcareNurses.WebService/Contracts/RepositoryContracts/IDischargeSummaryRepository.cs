using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDischargeSummaryRepository
    {
        Task<bool> AddDischargeSummary(DischargeSummary model);
        Task<List<DischargeSummary>> GetAllDischargeSummaries();
        Task<List<DischargeSummary>> GetDischargeSummariesByNurseId(string nurseId);
        Task<List<DischargeSummary>> GetDischargeSummariesByEmployerId(string employerId);
        Task<DischargeSummary> GetDischargeSummaryByJobId(int jobId);
        Task<DischargeSummary> GetDischargeSummariesById(int id);
        Task<bool> UpdateDischargeSummary(DischargeSummary model);
        Task<bool> DeleteDischargeSummary(DischargeSummary dischargeSummary);
    }
}
