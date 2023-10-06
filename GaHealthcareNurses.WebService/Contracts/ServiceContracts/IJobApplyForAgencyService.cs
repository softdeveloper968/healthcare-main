using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface IJobApplyForAgencyService
    {
        Task<IEnumerable<JobApplyForAgency>> Get();
        Task<JobApplyForAgency> GetById(int id);
        Task<IEnumerable<JobApplyForAgency>> GetByJobId(int id);
        Task<IEnumerable<JobApplyForAgency>> GetByJobIdAndStatusId(int jobId, int statusId);
        Task<IEnumerable<JobApplyForAgency>> GetByEmployerId(string id);
        Task<IEnumerable<JobApplyForAgency>> GetByStatusId(string employerId, int statusId);
        Task<List<GetActiveServiceRequestsResponseModel>> GetActiveServiceRequests(string employerId);
        Task<JobApplyForAgency> Add(JobApplyForAgency job);
        Task<JobApplyForAgency> Update(JobApplyForAgency job);
        Task Delete(JobApplyForAgency job);
        Task<JobApplyForAgency> ApplyJob(int jobApplyId,string prefferedRate);
        Task<JobApplyForAgency> HireAgency(int jobApplyId);
        Task<List<GetHiredNursesResponseModel>> GetHiredNurses(string employerId);

    }
}
