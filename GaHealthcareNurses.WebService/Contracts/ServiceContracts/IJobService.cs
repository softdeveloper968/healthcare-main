using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface IJobService
    {
        // Task<GetJobsForAgencyViewModel> GetJobsForAgency(PaginationFilter paginationFilter);
        Task<IEnumerable<Job>> GetJobsForAgency(int skip,int take,string filter);
        Task<int> TotalCount(string filter);
        Task<IEnumerable<Job>> Get(PaginationFilter paginationFilter);
        Task<Job> GetById(int id);
        Task<IEnumerable<Job>> GetByClientId(string id);
        Task<JobApplyForAgency> GetByJobAndStatusId(int jobId,int statusId);
        Task<Job> Add(Job job);
        Task Delete(Job job);
        Task Update(Job job);
        Task<Job> ClientRating(ClientRatingViewModel model);
        Task<Job> CreateServiceRequest(CreateServiceRequestViewModel model);
        Task<Job> CreateDuplicateServiceRequest(int jobId, string employerId);
        Task<List<GetCareRecipientListResponseModel>> GetCareRecipientsByEmployerId(string employerId);
        Task<List<GetServiceRequestListResponseModel>> GetJobsForEmployer(string employerId);
        Task<List<GetServiceRequestListResponseModel>> GetJobsForNurse(string nurseId);
        Task<List<GetServiceRequestListResponseModel>> GetJobsForClient(string clientId);
        Task<List<GetServiceRequestListResponseModel>> GetJobsForAdmin();
        Task<bool> DeleteServiceRequest(int jobId);
        Task<CreateServiceRequestViewModel> GetServiceRequestById(int jobId);
        Task<bool> UpdateServiceRequest(CreateServiceRequestViewModel model);
    }
}
