using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaHealthcareNurses.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarePlanController : ControllerBase
    {
        private readonly ICarePlanService _carePlanService;

        public CarePlanController(ICarePlanService carePlanService)
        {
            _carePlanService = carePlanService;
        }

        [HttpGet("CreateCarePlan")]
        public async Task<IActionResult> CreateCarePlan(int jobId, string employerId)
        {
            try
            {
                await _carePlanService.CreatePlan(new CarePlan
                {
                    JobId = jobId,
                    EmployerId = employerId,
                    EntryDate = DateTime.UtcNow
                });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Care plan created successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetCarePlanByNurseId")]
        public async Task<IActionResult> GetCarePlanByNurseId(string nurseId)
        {
            try
            {
                if (string.IsNullOrEmpty(nurseId))
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse is null" });
                var carePlans = await _carePlanService.GetCarePlanByNurseId(nurseId);
                return new JsonResult(new Response<List<GetCarePlanListResponseModel>> { Status = "Success", Data = carePlans });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetCarePlanByEmployerId")]
        public async Task<IActionResult> GetCarePlanByEmployerId(string employerId)
        {
            try
            {
                if (string.IsNullOrEmpty(employerId))
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Employer is null" });
                var carePlans = await _carePlanService.GetCarePlanByEmployerId(employerId);
                return new JsonResult(new Response<List<GetCarePlanListResponseModel>> { Status = "Success", Data = carePlans });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetCarePlanById")]
        public async Task<IActionResult> GetCarePlanById(int id)
        {
            try
            {
                var carePlan = await _carePlanService.GetCarePlanById(id);
                if (carePlan == null)
                    return new JsonResult(new Response<CarePlanRequestModel> { Status = "Error", Message = "Please enter valid care plan id" });
                return new JsonResult(new Response<CarePlanRequestModel> { Status = "Success", Data = carePlan });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetCarePlanByJobId")]
        public async Task<IActionResult> GetCarePlanByJobId(int jobId)
        {
            try
            {
                var carePlan = await _carePlanService.GetCarePlanByJobId(jobId);
                if (carePlan == null)
                    return new JsonResult(new Response<CarePlanRequestModel> { Status = "Error", Message = "Please enter valid care plan id" });
                return new JsonResult(new Response<CarePlanRequestModel> { Status = "Success", Data = carePlan });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("UpdateCarePlan")]
        public async Task<IActionResult> UpdateCarePlan(CarePlanRequestModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _carePlanService.UpdateCarePlan(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Care plan not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Care plan updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpDelete("DeleteCarePlan")]
        public async Task<IActionResult> DeleteCarePlan(int id)
        {
            try
            {
                var result = await _carePlanService.DeleteCarePlan(id);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Care plan not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Care plan deleted successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("AssignCarePlan")]
        public async Task<IActionResult> AssignCarePlan(AssignCarePlanViewModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _carePlanService.AssignCarePlan(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Care plan not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Care plan assigned successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetCarePlanPDFBytes/{id}")]
        public async Task<IActionResult> GetCarePlanPDFBytes(int id)
        {
            try
            {
                return new JsonResult(await _carePlanService.GetCarePlanPDFBytes(id));
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
