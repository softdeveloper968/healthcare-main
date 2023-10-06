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
    public class AdultAssessmentController : ControllerBase
    {
        private readonly IAdultAssessmentService _adultAssessmentService;

        public AdultAssessmentController(IAdultAssessmentService adultAssessmentService)
        {
            _adultAssessmentService = adultAssessmentService;
        }

        [HttpGet("CreateAssessment")]
        public async Task<IActionResult> CreateAssessment(int jobId, string employerId)
        {
            try
            {
                await _adultAssessmentService.CreateAssessment(new AdultAssessment
                {
                    JobId = jobId,
                    EmployerId = employerId
                });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Assessment created successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetAssessmentByNurseId")]
        public async Task<IActionResult> GetAssessmentByNurseId(string nurseId)
        {
            try
            {
                if (string.IsNullOrEmpty(nurseId))
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse is null" });
                var assessments = await _adultAssessmentService.GetAssessmentByNurseId(nurseId);
                return new JsonResult(new Response<List<GetAdultAssessmentListResponseModel>> { Status = "Success", Data = assessments });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetAssessmentByEmployerId")]
        public async Task<IActionResult> GetAssessmentByEmployerId(string employerId)
        {
            try
            {
                if (string.IsNullOrEmpty(employerId))
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Employer is null" });
                var assessments = await _adultAssessmentService.GetAssessmentByEmployerId(employerId);
                return new JsonResult(new Response<List<GetAdultAssessmentListResponseModel>> { Status = "Success", Data = assessments });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetAssessmentById")]
        public async Task<IActionResult> GetAssessmentById(int id)
        {
            try
            {
                var assessment = await _adultAssessmentService.GetAssessmentById(id);
                if (assessment == null)
                    return new JsonResult(new Response<AdultAssessmentRequestModel> { Status = "Error", Message = "Please enter valid assessment id" });
                return new JsonResult(new Response<AdultAssessmentRequestModel> { Status = "Success", Data = assessment });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetAssessmentByJobId")]
        public async Task<IActionResult> GetAssessmentByJobId(int id)
        {
            try
            {
                var assessment = await _adultAssessmentService.GetAssessmentByJobId(id);
                if (assessment == null)
                    return new JsonResult(new Response<AdultAssessmentRequestModel> { Status = "Error", Message = "Job Id is null" });
                return new JsonResult(new Response<AdultAssessmentRequestModel> { Status = "Success", Data = assessment });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("UpdateAssessment")]
        public async Task<IActionResult> UpdateAssessment(AdultAssessmentRequestModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _adultAssessmentService.UpdateAssessment(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Assessment not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Assessment updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpDelete("DeleteAssessment")]
        public async Task<IActionResult> DeleteAssessment(int id)
        {
            try
            {
                var result = await _adultAssessmentService.DeleteAssessment(id);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Assessment not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Assessment deleted successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("AssignAssessment")]
        public async Task<IActionResult> AssignAssessment(AssignAdultAssessmentViewModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _adultAssessmentService.AssignAssessment(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Assessment not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Assessment assigned successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
