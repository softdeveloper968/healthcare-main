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
    public class NurseCommentController : ControllerBase
    {
        private readonly INurseCommentService _nurseCommentService;

        public NurseCommentController(INurseCommentService nurseCommentService)
        {
            _nurseCommentService = nurseCommentService;
        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(AddCommentViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });
                }
                var result = await _nurseCommentService.AddComment(model);
                if (!result)
                {
                    return new JsonResult(new Response<bool> { Status = "Error", Data = result, Message = "Please check the details and try again" });
                }
                return new JsonResult(new Response<bool> { Status = "Success", Data = result, Message = "Comment saved successfully!" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetCommentsForNurse")]
        public async Task<IActionResult> GetCommentsForNurse(string nurseId, int jobId)
        {
            try
            {
                if (string.IsNullOrEmpty(nurseId))
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Please enter nurse id" });
                }
                if (jobId == 0)
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Please enter job id" });
                }
                var result = await _nurseCommentService.GetCommentsForNurse(nurseId, jobId);
                return new JsonResult(new Response<List<GetNurseCommentResponseModel>> { Status = "Success", Data = result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetCommmentsForAgency")]
        public async Task<IActionResult> GetCommmentsForAgency(string employerId)
        {
            try
            {
                if (string.IsNullOrEmpty(employerId))
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Please enter employer id" });
                }
                var result = await _nurseCommentService.GetCommentsForAgency(employerId);
                return new JsonResult(new Response<List<GetAgenyCommentResponseModel>> { Status = "Success", Data = result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("UpdateAgencyResponse")]
        public async Task<IActionResult> UpdateAgencyResponse(AgencyResponseRequestModel model)
        {
            try
            {
                if (model == null)
                {
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });
                }
                var result = await _nurseCommentService.UpdateAgencyResponse(model);
                if (!result)
                {
                    return new JsonResult(new Response<bool> { Status = "Error", Data = result, Message = "Please check the details and try again" });
                }
                return new JsonResult(new Response<bool> { Status = "Success", Data = result, Message = "Response saved successfully!" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("ReadNurseComment")]
        public async Task<IActionResult> ReadNurseComment(int nurseCommentId)
        {
            try
            {
                var result = await _nurseCommentService.ReadNurseComment(nurseCommentId);
                if (!result)
                {
                    return new JsonResult(new Response<bool> { Status = "Error", Data = result, Message = "Please check the details and try again" });
                }
                return new JsonResult(new Response<bool> { Status = "Success", Data = result, Message = "Comment read!" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
