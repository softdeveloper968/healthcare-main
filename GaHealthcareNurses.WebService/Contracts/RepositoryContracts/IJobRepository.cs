using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IJobRepository
    {
        //  Task<GetJobsForAgencyViewModel> GetJobsForAgency(PaginationFilter paginationFilter);
        Task<IEnumerable<Job>> GetJobsForAgency(int skip,int take,string filter);
        Task<int> TotalCount(string filter);
        Task<IEnumerable<Job>> Get(PaginationFilter paginationFilter);
        Task<Job> GetById(int id);
        Task<IEnumerable<Job>> GetByClientId(string id);
        Task<JobApplyForAgency> GetByJobAndStatusId(int jobId, int statusId);
        Task<Job> Add(Job job);
        Task Update(Job job);
        Task Delete(Job job);
        Task<List<Job>> GetJobsByEmployerId(string employerId);
        Task<List<Job>> GetJobsForEmployer(string employerId);
        Task<List<Job>> GetJobsForNurse(string nurseId);
        Task<List<Job>> GetJobsForClient(string clientId);
        Task<List<Job>> GetJobsForAdmin();
        Task<Job> GetServiceRequestById(int jobId);
    }
}
