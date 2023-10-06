﻿using AutoMapper;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GaHealthcareNurses.WebService.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplyForEmployerController : ControllerBase
    {
        private readonly IJobApplyForAgencyService _jobApplyForAgencyService;
        private readonly IMapper _mapper;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly INurseService _nurseService;
        public JobApplyForEmployerController(IJobApplyForAgencyService jobApplyForAgencyService, IMapper mapper, INurseService nurseService, IPushNotificationService pushNotificationService)
        {
            _jobApplyForAgencyService = jobApplyForAgencyService;
            _mapper = mapper;
            _nurseService = nurseService;
            _pushNotificationService = pushNotificationService;
        }


        //// GET: api/JobApply/GetAll

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        var jobsApplied = await _jobApplyService.Get();
        //        return new JsonResult(new Response<IEnumerable<JobApply>> { Status = "Success", Data = jobsApplied });
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
        //    }
        //}

        // GET: api/JobApplyForEmployer/GetByJobId
        [HttpGet]
        [Route("GetByJobId")]
        public async Task<IActionResult> GetByJobId(int id)
        {
            var jobsApplied = await _jobApplyForAgencyService.GetByJobId(id);
            if (jobsApplied == null)
                return new JsonResult(new Response<string> { Status = "Error", Message = "Invalid job id" });

            return new JsonResult(new Response<IEnumerable<JobApplyForAgency>> { Status = "Success", Data = jobsApplied });
        }


        // GET: api/JobApplyForEmployer/GetByEmployerId
        [HttpGet]
        [Route("GetByEmployerId")]
        public async Task<IActionResult> GetByEmployerId(string id)
        {
            var jobsApplied = await _jobApplyForAgencyService.GetByEmployerId(id);
            if (jobsApplied == null)
                return new JsonResult(new Response<string> { Status = "Error", Message = "Invalid employer id" });

            return new JsonResult(new Response<IEnumerable<JobApplyForAgency>> { Status = "Success", Data = jobsApplied });
        }

        // GET: api/JobApplyForEmployer/GetByStatusId
        [HttpGet]
        [Route("GetByStatusId")]
        public async Task<IActionResult> GetByStatusId(string employerId, int statusId)
        {
            var jobsApplied = await _jobApplyForAgencyService.GetByStatusId(employerId, statusId);
            if (jobsApplied == null)
                return new JsonResult(new Response<string> { Status = "Error", Message = "Invalid employer id" });

            return new JsonResult(new Response<IEnumerable<JobApplyForAgency>> { Status = "Success", Data = jobsApplied });
        }

        [HttpGet]
        [Route("GetActiveServiceRequests")]
        public async Task<IActionResult> GetActiveServiceRequests(string employerId)
        {
            try
            {
                var activeServiceRequests = await _jobApplyForAgencyService.GetActiveServiceRequests(employerId);
                return new JsonResult(new Response<List<GetActiveServiceRequestsResponseModel>> { Status = "Success", Data = activeServiceRequests });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }


        // GET: api/JobApplyForEmployer/GetById
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var jobApply = await _jobApplyForAgencyService.GetById(id);
            if (jobApply == null)
                return new JsonResult(new Response<string> { Status = "Error", Message = "Invalid jobApply id" });

            return new JsonResult(new Response<JobApplyForAgency> { Status = "Success", Data = jobApply });
        }


        //   POST: api/JobApplyForEmployer/Post
        [Route("Post")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobApplyForAgencyViewModel jobApply)
        {
            try
            {
                if (jobApply == null)
                    return BadRequest("JobApply is null");

                var jobs = _mapper.Map<JobApplyForAgency>(jobApply);

                var jobApplied = await _jobApplyForAgencyService.Add(jobs);

                if (jobApplied == null)
                    return new JsonResult(new Response<JobApply> { Status = "Error", Message = "Job apply failed! Please check job details and try again." });

                return new JsonResult(new Response<JobApplyForAgency> { Status = "Success", Message = "Job applied successfully!", Data = jobApplied });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        //POST: api/JobApplyForEmployer/ApplyJob
        [Route("ApplyJob")]
        [HttpPost]
        public async Task<IActionResult> ApplyJob([FromBody] JobApplyForEmployerViewModel jobApply)
        {
            try
            {
                if (jobApply == null)
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "JobApply is null" });
                }

                var jobApplied = await _jobApplyForAgencyService.ApplyJob(Convert.ToInt32(jobApply.JobApplyId), jobApply.PrefferedRate);
                if (jobApplied != null)
                    return new JsonResult(new Response<JobApplyForAgency> { Status = "Success", Data = jobApplied });

                return new JsonResult(new Response<string> { Status = "Error", Message = "Please check details and try again." });

            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        //POST: api/JobApplyForEmployer/HireAgency
        [Route("HireAgency")]
        [HttpGet]
        public async Task<IActionResult> HireAgency(int jobApplyId)
        {
            try
            {
                if (jobApplyId == 0)
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "JobApply is null" });
                }

                var job = await _jobApplyForAgencyService.HireAgency(jobApplyId);
                if (job != null)
                {
                    return new JsonResult(new Response<JobApplyForAgency> { Status = "Success", Data = job });
                }

                return new JsonResult(new Response<string> { Status = "Error", Message = "Please check details and try again." });

            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [Route("GetHiredNurses")]
        [HttpGet]
        public async Task<IActionResult> GetHiredNurses(string employerId)
        {
            try
            {
                var nurses = await _jobApplyForAgencyService.GetHiredNurses(employerId);
                return new JsonResult(new Response<List<GetHiredNursesResponseModel>> { Status = "Success", Data = nurses });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
