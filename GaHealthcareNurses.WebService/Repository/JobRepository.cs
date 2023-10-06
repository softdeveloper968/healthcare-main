using Contracts;
using GaHealthcareNurses.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using GaHealthcareNurses.Entity.Models;
using Microsoft.EntityFrameworkCore;
using GaHealthcareNurses.Entity.ViewModels;
using System.Globalization;
using System;

namespace Repository
{
    public class JobRepository : IJobRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;
        // private readonly IRequiredServiceRepository _requiredServiceRepository;

        #region Contructor For JobRepository
        public JobRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
            //  _requiredServiceRepository = requiredServiceRepository;
        }
        #endregion

        #region Implementing Interface
        public async Task<Job> Add(Job job)
        {
            await _gaHealthcareNursesContext.Job.AddAsync(job);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return job;
        }

        public async Task<int> TotalCount(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return await _gaHealthcareNursesContext.Job.CountAsync();

            return await _gaHealthcareNursesContext.Job.Where(x => x.Description.Contains(filter) || x.CareRecipient.City.Name.Contains(filter) || x.JobTitle.Title.Contains(filter) || x.PostedTime.ToString().Contains(filter)).CountAsync();
        }

        public async Task Delete(Job job)
        {
            _gaHealthcareNursesContext.Job.Remove(job);
            await _gaHealthcareNursesContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Job>> Get(PaginationFilter paginationFilter)
        {
            //CultureInfo culture = new CultureInfo("en-US");
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            var jobs =  _gaHealthcareNursesContext.Job.Where(x => x.SentToNurse == true)
            .Include(x => x.CareRecipient)
            .Include(x => x.CareRecipient.City)
            .Include(x => x.Client)
            .Include(x => x.Resource)
            .Include(x => x.JobTitle)
            .Include(x => x.Employer)
            .Include(x => x.VisitNotes)
            .Include(x => x.Status)
            .Include(x => x.JobApplies).OrderByDescending(x => x.JobId).Skip(skip).Take(paginationFilter.PageSize)/*.ToList().Where(x => (Convert.ToDateTime(x.CareRecipient.EndDate, culture)> DateTime.UtcNow) && (x.SentToNurse == true)).Skip(skip).Take(paginationFilter.PageSize)*/.ToList();
            return jobs;
        }

        public async Task<IEnumerable<Job>> GetJobsForAgency(int skip, int take, string filter)
        {
            if (filter != null)
            {
                var filteredRecords = await _gaHealthcareNursesContext.Job.Where(x => x.Description.Contains(filter) || x.CareRecipient.City.Name.Contains(filter) || x.JobTitle.Title.Contains(filter) || x.PostedTime.ToString().Contains(filter)).Include(x => x.CareRecipient).Include(x => x.CareRecipient.City).Include(x => x.Resource).Include(x => x.JobTitle).Include(x => x.Employer).Include(x => x.VisitNotes).Include(x => x.Status).OrderByDescending(x => x.JobId).ToListAsync();
                return filteredRecords.Skip(skip).Take(take).ToList();
            }
            return await _gaHealthcareNursesContext.Job.Include(x => x.CareRecipient).Include(x => x.CareRecipient.City).Include(x => x.Resource).Include(x => x.JobTitle).Include(x => x.Employer).Include(x => x.VisitNotes).Include(x => x.Status).OrderByDescending(x => x.JobId).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Job> GetById(int id)
        {
          
            //var task = await _gaHealthcareNursesContext.TaskList.Where(blog => blog.JobId == id).ToListAsync();

            //var taskName = task.GroupBy(t => t.TaskName).Select(b => b).ToList();
            var job = await _gaHealthcareNursesContext.Job.Where(x => x.JobId == id).Include(x => x.Resource).Include(x=>x.JobTitle).Include(x => x.Employer).Include(x => x.VisitNotes).Include(x => x.CareRecipient).Include(x => x.CareRecipient.City).Include(x => x.CareRecipient.ServiceList).Include(x=>x.TaskList).FirstOrDefaultAsync();
         
            return job;
        }

        public async Task<IEnumerable<Job>> GetByClientId(string id)
        {
            return await _gaHealthcareNursesContext.Job.Where(x => x.ClientId == id).Include(x => x.JobApplies).ThenInclude(x => x.Nurse).Include(x => x.Resource).Include(x => x.JobTitle).Include(x => x.Employer).Include(x => x.VisitNotes).Include(x => x.CareRecipient).Include(x => x.CareRecipient.City).Include(x => x.CareRecipient.ServiceList).ToListAsync();
        }
        public async Task Update(Job job)
        {
            _gaHealthcareNursesContext.Job.Update(job);
            await _gaHealthcareNursesContext.SaveChangesAsync();
        }

        public async Task<JobApplyForAgency> GetByJobAndStatusId(int jobId, int statusId)
        {
            return await _gaHealthcareNursesContext.JobApplyForAgency.Where(x => x.JobId == jobId && x.StatusId == statusId).FirstOrDefaultAsync();
        }

        public async Task<List<Job>> GetJobsByEmployerId(string employerId)
        {
            return await _gaHealthcareNursesContext.Job.Include(x => x.CareRecipient).ThenInclude(x => x.State).Include(x => x.CareRecipient.City).Include(x => x.JobApplies).ThenInclude(x => x.Nurse).Where(x => x.EmployerId == employerId).ToListAsync();
        }

        public async Task<List<Job>> GetJobsForEmployer(string employerId)
        {
            return await _gaHealthcareNursesContext.Job.Include(x => x.CareRecipient).ThenInclude(x => x.City).ThenInclude(x => x.State).Include(x => x.JobTitle).Include(x => x.CareRecipient.ServiceList).Include(x => x.JobApplies).ThenInclude(x => x.Nurse).Include(x => x.JobApplyForAgencies.Where(x => x.EmployerId == employerId)).Include(x => x.Employer).Include(x => x.DischargeSummaries).Include(x => x.SupervisoryVisits).Include(x => x.AdultAssessments).Include(x => x.CarePlans).Where(x => x.JobApplyForAgencies.Any(y => y.EmployerId == employerId && (y.StatusId == 13 || y.StatusId == 14))).ToListAsync();
        }

        public async Task<List<Job>> GetJobsForNurse(string nurseId)
        {
            return await _gaHealthcareNursesContext.Job.Include(x => x.CareRecipient).ThenInclude(x => x.City).ThenInclude(x => x.State).Include(x => x.JobTitle).Include(x => x.CareRecipient.ServiceList).Include(x => x.JobApplies.Where(x => x.NurseId == nurseId)).ThenInclude(x => x.Nurse).Include(x => x.JobApplyForAgencies).Include(x => x.Employer).Include(x => x.DischargeSummaries).Include(x => x.SupervisoryVisits).Include(x => x.AdultAssessments).Include(x => x.CarePlans).Where(x => x.JobApplies.Any(y => y.NurseId == nurseId && (y.StatusId == 1 || y.StatusId == 7))).ToListAsync();
        }

        public async Task<List<Job>> GetJobsForClient(string clientId)
        {
            return await _gaHealthcareNursesContext.Job.Include(x => x.CareRecipient).ThenInclude(x => x.City).ThenInclude(x => x.State).Include(x => x.JobTitle).Include(x => x.CareRecipient.ServiceList).Include(x => x.JobApplies).ThenInclude(x => x.Nurse).Include(x => x.JobApplyForAgencies).Include(x => x.Employer).Include(x => x.DischargeSummaries).Include(x => x.SupervisoryVisits).Include(x => x.AdultAssessments).Include(x => x.CarePlans).Where(x => x.ClientId == clientId).ToListAsync();
        }

        public async Task<List<Job>> GetJobsForAdmin()
        {
            return await _gaHealthcareNursesContext.Job.Include(x => x.CareRecipient).ThenInclude(x => x.City).ThenInclude(x => x.State).Include(x => x.JobTitle).Include(x => x.CareRecipient.ServiceList).Include(x => x.JobApplies).ThenInclude(x => x.Nurse).Include(x => x.JobApplyForAgencies).Include(x => x.Employer).Include(x => x.DischargeSummaries).Include(x => x.SupervisoryVisits).Include(x => x.AdultAssessments).Include(x => x.CarePlans).ToListAsync();
        }

        public async Task<Job> GetServiceRequestById(int jobId)
        {
            return await _gaHealthcareNursesContext.Job.Include(x => x.DischargeSummaries).Include(x => x.SupervisoryVisits).Include(x => x.AdultAssessments).Include(x => x.CareRecipient).Include(x => x.JobApplies.Where(y => y.StatusId == 1)).Include(x => x.JobApplyForAgencies.Where(y => y.StatusId == 13)).Include(x => x.TaskList).FirstOrDefaultAsync(x => x.JobId == jobId);
        }
        #endregion
    }
}
