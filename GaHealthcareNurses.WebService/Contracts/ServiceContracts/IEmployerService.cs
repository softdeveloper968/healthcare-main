using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface IEmployerService
    {
        Task<int> TotalCount(string filter);
        Task<IEnumerable<Employer>> Get(int skip, int take, string filter);
        Task<IEnumerable<Employer>> GetAll();
        Task<Employer> GetById(string id);
        Task<bool> GetByName(Employer employer);
        Task Add(Employer employer);
        Task<bool> Delete(string id);
        Task<Employer> Update(EmployerViewModel employer);
        Task<List<Employer>> GetAgenciesForBid(int jobId);
        Task<List<AgencyServedCounties>> GetAgencyServedCounties(string employerId);
        Task<bool> SetPermissions(SetPermissionsViewModel model);
        Task<List<Nurse>> GetNursesList(string employerId);
    }
}
