using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISupervisoryVisitRepository
    {
        Task<bool> AddSupervisoryVisit(SupervisoryVisit model);
        Task<List<SupervisoryVisit>> GetSupervisoryVisitByNurseId(string nurseId);
        Task<List<SupervisoryVisit>> GetSupervisoryVisitByEmployerId(string employerId);
        Task<SupervisoryVisit> GetSupervisoryVisitById(int id);
        Task<bool> UpdateSupervisoryVisit(SupervisoryVisit model);
        Task<bool> DeleteSupervisoryVisit(SupervisoryVisit model);
        Task<SupervisoryVisit> GetById(int id);
    }
}
