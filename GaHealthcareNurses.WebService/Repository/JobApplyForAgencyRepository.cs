﻿using Contracts;
using GaHealthcareNurses.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using GaHealthcareNurses.Entity.Models;
using Microsoft.EntityFrameworkCore;
using GaHealthcareNurses.Entity.ViewModels;

namespace Repository
{
    public class JobApplyForAgencyRepository : IJobApplyForAgencyRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;
        // private readonly IRequiredServiceRepository _requiredServiceRepository;

        #region Contructor For JobApplyForAgencyRepository
        public JobApplyForAgencyRepository(GaHealthcareNursesContext context)
        {
            _gaHealthcareNursesContext = context;
            //  _requiredServiceRepository = requiredServiceRepository;
        }
        #endregion

        #region Implementing Interface
        public async Task<JobApplyForAgency> Add(JobApplyForAgency job)
        {
            
            await _gaHealthcareNursesContext.JobApplyForAgency.AddAsync(job);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return job;
        }


        public async Task Delete(JobApplyForAgency job)
        {
            _gaHealthcareNursesContext.JobApplyForAgency.Remove(job);
            await _gaHealthcareNursesContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobApplyForAgency>> Get()
        {
            return await _gaHealthcareNursesContext.JobApplyForAgency.Include(x=>x.Status).Include(x=>x.Employer).Include(x=>x.Job).ToListAsync();
        }

        public async Task<JobApplyForAgency> GetById(int id)
        {
            var job = await _gaHealthcareNursesContext.JobApplyForAgency.Where(x => x.Id == id).Include(x=>x.Status).Include(x=>x.Job.Client).Include(x=>x.Job.CareRecipient).Include(x=>x.Job.CareRecipient.City).Include(x=>x.Job.JobTitle).Include(x=>x.Job).Include(x=>x.Employer).FirstOrDefaultAsync();
            return job;
        }

        public async Task<IEnumerable<JobApplyForAgency>> GetByJobId(int id)
        {
            var jobs = await _gaHealthcareNursesContext.JobApplyForAgency.Where(x => x.JobId == id).Include(x => x.Status).Include(x => x.Job).Include(x=>x.Employer).ToListAsync();
            return jobs;
        }

        public async Task<IEnumerable<JobApplyForAgency>> GetByJobIdAndStatusId(int jobId, int statusId)
        {
            var jobs = await _gaHealthcareNursesContext.JobApplyForAgency.Include(x => x.Employer).Where(x => x.JobId == jobId && x.StatusId == statusId).ToListAsync();
            return jobs;
        }

        public async Task<IEnumerable<JobApplyForAgency>> GetByStatusId(string employerId,int statusId)
        {
            var jobs = await _gaHealthcareNursesContext.JobApplyForAgency.Where(x => x.EmployerId== employerId && x.StatusId==statusId).Include(x => x.Status).Include(x => x.Job).ThenInclude(x => x.TaskList).Include(x=>x.Job.CareRecipient).Include(x=>x.Job.CareRecipient.City).Include(x=>x.Job.CareRecipient.State).Include(x=>x.Job.CareRecipient.ServiceList).Include(x => x.Employer).Include(x=>x.Job.Client).Include(x=>x.Job.JobTitle).ToListAsync();
            return jobs;
        }


        public async Task<IEnumerable<JobApplyForAgency>> GetByEmployerId(string id)
        {
            var jobs = await _gaHealthcareNursesContext.JobApplyForAgency.Where(x => x.EmployerId == id).Include(x => x.Status).Include(x => x.Job.CareRecipient).Include(x=>x.Job.Client).Include(x=>x.Job.Employer).Include(x=>x.Job.CareRecipient.City).Include(x=>x.Job.JobTitle).Include(x => x.Employer).ToListAsync();
            return jobs;
        }

        public async Task<JobApplyForAgency> Update(JobApplyForAgency job)
        {
            _gaHealthcareNursesContext.JobApplyForAgency.Update(job);
            await _gaHealthcareNursesContext.SaveChangesAsync();
            return job;
        }

        public async Task<List<GetHiredNursesResponseModel>> GetHiredNurses(string employerId)
        {
            var nurses = await _gaHealthcareNursesContext.Job.Include(x => x.JobApplies).ThenInclude(x => x.Nurse).Where(x => x.EmployerId == employerId && x.JobApplies.Any(y => y.StatusId == 1)).Select(x => new GetHiredNursesResponseModel { Id = x.JobApplies.FirstOrDefault(x => x.StatusId == 1).Nurse.Id, Name = $"{x.JobApplies.FirstOrDefault(x => x.StatusId == 1).Nurse.FirstName} {x.JobApplies.FirstOrDefault(x => x.StatusId == 1).Nurse.LastName}" }).Distinct().ToListAsync();
            return nurses;
        }
        #endregion
    }
}
