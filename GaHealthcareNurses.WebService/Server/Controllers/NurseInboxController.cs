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
    public class NurseInboxController : ControllerBase
    {
        private readonly INurseInboxService _nurseInboxService;

        public NurseInboxController(INurseInboxService nurseInboxService)
        {
            _nurseInboxService = nurseInboxService;
        }

        [HttpGet("GetNurseMessages")]
        public async Task<IActionResult> GetNurseMessages(string nurseId)
        {
            try
            {
                if (string.IsNullOrEmpty(nurseId))
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse is null" });
                var nurseMessages = await _nurseInboxService.GetNurseMessages(nurseId);
                return new JsonResult(new Response<List<GetNurseMessagesResponseModel>> { Status = "Success", Data = nurseMessages });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("ReadMessage")]
        public async Task<IActionResult> ReadMessage(ReadMessageViewModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });
                var result = await _nurseInboxService.ReadMessage(model);
                if(!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Message not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Message updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpDelete("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                var result = await _nurseInboxService.DeleteMessage(id);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Message not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Message deleted successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
