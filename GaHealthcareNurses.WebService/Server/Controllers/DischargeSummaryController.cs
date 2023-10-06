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
    public class DischargeSummaryController : ControllerBase
    {
        private readonly IDischargeSummaryService _dischargeSummaryService;

        public DischargeSummaryController(IDischargeSummaryService dischargeSummaryService)
        {
            _dischargeSummaryService = dischargeSummaryService;
        }

        //[HttpPost("CompleteServiceRequestFromMobile")]
        //public async Task<IActionResult> CompleteServiceRequestFromMobile(CompleteServiceRequestFromMobileRequestModel model)
        //{
        //    try
        //    {
        //        if (model == null)
        //            return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });
        //        if (string.IsNullOrEmpty(model.NurseId) || model.JobId == 0)
        //            return new JsonResult(new Response<string> { Status = "Error", Message = "Enter valid nurse id or job id" });

        //        var result = await _dischargeSummaryService.CompleteServiceRequestFromMobile(model);
        //        return new JsonResult(new Response<string> { Status = "Success", Message = result });

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
        //    }
        //}

        [HttpGet("CreateDischargeSummary")]
        public async Task<IActionResult> CreateDischargeSummary(int jobId, string employerId)
        {
            try
            {
                await _dischargeSummaryService.CreateDischargeSummary(new DischargeSummary
                {
                    JobId = jobId,
                    EmployerId = employerId,
                    DateCreated = DateTime.UtcNow
                });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Discharge summary created successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetAllDischargeSummaries")]
        public async Task<IActionResult> GetAllDischargeSummaries()
        {
            try
            {
                var dischargeSummaries = await _dischargeSummaryService.GetAllDischargeSummaries();
                return new JsonResult(new Response<List<GetDischargeSummaryListResponseModel>> { Status = "Success", Data = dischargeSummaries });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetDischargeSummariesByNurseId")]
        public async Task<IActionResult> GetDischargeSummariesByNurseId(string nurseId)
        {
            try
            {
                if (string.IsNullOrEmpty(nurseId))
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse is null" });
                var dischargeSummaries = await _dischargeSummaryService.GetDischargeSummariesByNurseId(nurseId);
                return new JsonResult(new Response<List<GetDischargeSummaryListResponseModel>> { Status = "Success", Data = dischargeSummaries });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetDischargeSummariesByEmployerId")]
        public async Task<IActionResult> GetDischargeSummariesByEmployerId(string employerId)
        {
            try
            {
                if (string.IsNullOrEmpty(employerId))
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Employer is null" });
                var dischargeSummaries = await _dischargeSummaryService.GetDischargeSummariesByEmployerId(employerId);
                return new JsonResult(new Response<List<GetDischargeSummaryListResponseModel>> { Status = "Success", Data = dischargeSummaries });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetDischargeSummariesById")]
        public async Task<IActionResult> GetDischargeSummariesById(int id)
        {
            try
            {
                var dischargeSummary = await _dischargeSummaryService.GetDischargeSummariesById(id);
                if (dischargeSummary == null)
                    return new JsonResult(new Response<DischargeSummaryRequestModel> { Status = "Error", Message = "Please enter valid discharge summary id" });
                return new JsonResult(new Response<DischargeSummaryRequestModel> { Status = "Success", Data = dischargeSummary });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetDischargeSummaryPDFBytes/{id}")]
        public async Task<IActionResult> GetDischargeSummaryPDFBytes(int id)
        {
            try
            {
                return new JsonResult(await _dischargeSummaryService.GetDischargeSummaryPDFBytes(id));
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("CompleteDischargeSummary")]
        public async Task<IActionResult> CompleteDischargeSummary(DischargeSummaryRequestModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _dischargeSummaryService.CompleteDischargeSummary(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Discharge summary not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Discharge summary completed successfully" });

            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("AcceptOrRejectDischargeSummary")]
        public async Task<IActionResult> AcceptOrRejectDischargeSummary(AcceptOrRejectDischargeSummaryRequestModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _dischargeSummaryService.AcceptOrRejectDischargeSummary(model);
                return new JsonResult(new Response<string> { Status = "Success", Message = result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpDelete("DeleteDischargeSummary")]
        public async Task<IActionResult> DeleteDischargeSummary(int id)
        {
            try
            {
                var result = await _dischargeSummaryService.DeleteDischargeSummary(id);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Discharge summary not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Discharge summary deleted successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("AssignDischargeSummary")]
        public async Task<IActionResult> AssignDischargeSummary(AssignDischargeSummaryViewModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _dischargeSummaryService.AssignDischargeSummary(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Discharge summary not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Discharge summary assigned successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
