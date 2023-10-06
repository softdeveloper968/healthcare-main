using AutoMapper;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GaHealthcareNurses.WebService.Controllers
{
    //   [Authorize(Roles = "Nurse")]
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private readonly INurseService _nurseService;
        private readonly IMapper _mapper;
        public NursesController(INurseService nurseService, IMapper mapper)
        {
            _nurseService = nurseService;
            _mapper = mapper;
        }

        // GET: api/GetAll
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var nurses = await _nurseService.GetAll();
            //var postsResponse = _mapper.Map<List<items>>(1.Item1);
            return Ok(nurses);
        }

        // GET: api/GetById
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                Nurse nurse = await _nurseService.GetById(id);
                if (nurse != null)
                {
                    return new JsonResult(new Response<Nurse> { Status = "Success", Data = nurse });
                }
                return new JsonResult(new Response<string> { Status = "Error", Message = "Invalid nurse id" });
            }
            catch(Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("ChangeUserJobAvailability")]
        public async Task<IActionResult> ChangeUserJobAvailability(JobAvailabilityViewModel jobAvailability)
        {
            var nurseData = await _nurseService.ChangeUserJobAvailability(jobAvailability);
            if (nurseData == null)
                return new JsonResult(new Response<string> { Status = "Error", Message = "Invalid user id" });

            return new JsonResult(new Response<Nurse> { Status = "Success", Data = nurseData });
        }

        [HttpGet]
        [Route("GetAllNurses")]
        public async Task<object> GetNursesByDecendingDate()
        {
            int allNursesCount = await _nurseService.GetNursesCount(string.Empty);
            var queryString = Request.Query;
            StringValues Skip;
            StringValues Take;
            StringValues Filter;
            int skip = (queryString.TryGetValue("$skip", out Skip)) ? Convert.ToInt32(Skip[0]) : 0;

            string filterValue = (queryString.TryGetValue("$filter", out Filter)) ? Convert.ToString(Filter[0])?.Split('\'')[1] : null;
            if (filterValue != null)
            {
                allNursesCount = await _nurseService.GetNursesCount(filterValue);
            }
            int top = (queryString.TryGetValue("$top", out Take)) ? Convert.ToInt32(Take[0]) : allNursesCount;

            var nurses = await _nurseService.GetbyCreatedDate(skip, top, filterValue);

            return Ok(new { Items = nurses, Count = allNursesCount });
        }

        [HttpGet]
        [Route("GetNursesWithState")]
        public async Task<IActionResult> GetNursesWithState()
        {
            var nurses = await _nurseService.GetNurses();
            return Ok(nurses);
        }
        
        [HttpPost]
        [Route("SendReferralEmail")]
        public async Task<IActionResult> SendReferralEmail(SendReferralViewModel sendReferralViewModel)
        {
            try
            {
                if (sendReferralViewModel == null)
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse referral is null" });
                }
                var nurseReferral = await _nurseService.AddReferral(sendReferralViewModel);
                return new JsonResult(new Response<string> { Status = "Success", Message = nurseReferral });
                //return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse is already registered or referral already sent" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("ClaimReward")]
        public async Task<IActionResult> ClaimReward(string nurseId)
        {
            try
            {
               var nurse = await _nurseService.ClaimReward(nurseId);
                return new JsonResult(new Response<string> { Status = "Success", Message = nurse });
              
            }
            catch(Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("BuyCourses")]
        public async Task<IActionResult> BuyCourses(string nurseId)
        {
            try
            {
               var redirectingUrl = await _nurseService.BuyCourses(nurseId);
                if(redirectingUrl != null)
                {
                    return new JsonResult(new Response<string> { Status = "Success", Data = redirectingUrl });
                }
                return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse is null"});
            }
            catch(Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("ExecutePayment")]
        public async Task<IActionResult> ExecutePayment(string paymentId, string payerId)
        {
            try
            {
               var isPaymentExecuted = await _nurseService.ExecutePayment(paymentId, payerId);
                if(isPaymentExecuted)
                 return new JsonResult(new Response<string> { Status = "Success", Message = "Payment executed successfully." });

                return new JsonResult(new Response<string> { Status = "Error", Message = "Payment execution failed." });
            }
            catch(Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("AddUpdateCnaSkills")]
        public async Task<IActionResult> AddUpdateCnaSkills(NurseCnaSkillsViewModel nurseCnaSkills)
        {
            try
            {
                if (nurseCnaSkills == null)
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "CNA skills are null" });
                }
               var result = await _nurseService.AddUpdateCnaSkills(nurseCnaSkills);
                if (!result)
                {
                    return new JsonResult(new Response<bool> { Status = "Error", Data = result, Message = "Please check the details and try again" });
                }
                return new JsonResult(new Response<bool> { Status = "Success", Data = result, Message = "CNA skills saved successfully!" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetCnaSkills")]
        public async Task<IActionResult> GetCnaSkills(string nurseId)
        {
            try
            {
                if (string.IsNullOrEmpty(nurseId))
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Please enter nurse id" });
                }
                var result = await _nurseService.GetCnaSkills(nurseId);
                return new JsonResult(new Response<NurseCnaSkillsViewModel> { Status = "Success", Data = result});
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllNursesIds")]
        public async Task<IActionResult> GetAllNursesIds()
        {
            try
            {
                var nursesIds = await _nurseService.GetAllNursesIds();
                return new JsonResult(new Response<List<GetAllNursesResponseModel>> { Status = "Success", Data = nursesIds });
            }
            catch(Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("UpdateNurseStatus")]
        public async Task<IActionResult> UpdateNurseStatus(UpdateNurseStatusViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });
                }
                var result = await _nurseService.UpdateNurseStatus(model);
                if(!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Nurse status updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetNursePersonalInformation")]
        public async Task<IActionResult> GetNursePersonalInformation(string nurseId)
        {
            try
            {
                var nurseData = await _nurseService.GetNursePersonalInformation(nurseId);
                if(nurseData == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Enter valid nurse id" });
                return new JsonResult(new Response<NurseUpdateViewModel> { Status = "Success", Data = nurseData });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("UpdateNursePersonalInformation")]
        public async Task<IActionResult> UpdateNursePersonalInformation(NurseUpdateViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });
                }
                var result = await _nurseService.UpdateNursePersonalInformation(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Nurse information updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
