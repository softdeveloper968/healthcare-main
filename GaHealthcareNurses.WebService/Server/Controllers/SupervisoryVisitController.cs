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
    public class SupervisoryVisitController : ControllerBase
    {
        private readonly ISupervisoryVisitService _supervisoryVisitService;

        public SupervisoryVisitController(ISupervisoryVisitService supervisoryVisitService)
        {
            _supervisoryVisitService = supervisoryVisitService;
        }

        [HttpGet("CreateSupervisoryVisit")]
        public async Task<IActionResult> CreateSupervisoryVisit(int jobId, string employerId)
        {
            try
            {
                await _supervisoryVisitService.CreateSupervisoryVisit(new SupervisoryVisit
                {
                    JobId = jobId,
                    EmployerId = employerId,
                    DateCreated = DateTime.UtcNow
                });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Supervisory visit created successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetSupervisoryVisitByNurseId")]
        public async Task<IActionResult> GetSupervisoryVisitByNurseId(string nurseId)
        {
            try
            {
                if (string.IsNullOrEmpty(nurseId))
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse is null" });
                var supervisoryVisits = await _supervisoryVisitService.GetSupervisoryVisitByNurseId(nurseId);
                return new JsonResult(new Response<List<GetSupervisoryVisitListResponseModel>> { Status = "Success", Data = supervisoryVisits });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetSupervisoryVisitByEmployerId")]
        public async Task<IActionResult> GetSupervisoryVisitByEmployerId(string employerId)
        {
            try
            {
                if (string.IsNullOrEmpty(employerId))
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Employer is null" });
                var supervisoryVisits = await _supervisoryVisitService.GetSupervisoryVisitByEmployerId(employerId);
                return new JsonResult(new Response<List<GetSupervisoryVisitListResponseModel>> { Status = "Success", Data = supervisoryVisits });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetSupervisoryVisitById")]
        public async Task<IActionResult> GetSupervisoryVisitById(int id)
        {
            try
            {
                var supervisoryVisit = await _supervisoryVisitService.GetSupervisoryVisitById(id);
                if (supervisoryVisit == null)
                    return new JsonResult(new Response<SupervisoryVisitRequestModel> { Status = "Error", Message = "Please enter valid supervisory visit id" });
                return new JsonResult(new Response<SupervisoryVisitRequestModel> { Status = "Success", Data = supervisoryVisit });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("UpdateSupervisoryVisit")]
        public async Task<IActionResult> UpdateSupervisoryVisit(SupervisoryVisitRequestModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _supervisoryVisitService.UpdateSupervisoryVisit(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Supervisory visit not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Supervisory visit updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpDelete("DeleteSupervisoryVisit")]
        public async Task<IActionResult> DeleteSupervisoryVisit(int id)
        {
            try
            {
                var result = await _supervisoryVisitService.DeleteSupervisoryVisit(id);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Supervisory visit not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Supervisory visit deleted successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("AssignSupervisoryVisit")]
        public async Task<IActionResult> AssignSupervisoryVisit(AssignSupervisoryVisitViewModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _supervisoryVisitService.AssignSupervisoryVisit(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Supervisory visit not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Supervisory visit assigned successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("GetSupervisoryVisitPDFBytes/{id}")]
        public async Task<IActionResult> GetSupervisoryVisitPDFBytes(int id)
        {
            try
            {
                return new JsonResult(await _supervisoryVisitService.GetSupervisoryVisitPDFBytes(id));
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
