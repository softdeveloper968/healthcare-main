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
    public class NotifyConfigurationController : ControllerBase
    {
        private readonly INotifyConfigurationService _notifyConfigurationService;

        public NotifyConfigurationController(INotifyConfigurationService notifyConfigurationService)
        {
            _notifyConfigurationService = notifyConfigurationService;
        }

        [HttpGet("GetConfigurationByEmployerId")]
        public async Task<IActionResult> GetConfigurationByEmployerId(string employerId)
        {
            try
            {
                var configuration = await _notifyConfigurationService.GetByEmployerId(employerId);
                return new JsonResult(new Response<NotifyConfigurationRequestModel> { Status = "Success", Data = configuration });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("AddUpdateConfiguration")]
        public async Task<IActionResult> AddUpdateConfiguration(NotifyConfigurationRequestModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _notifyConfigurationService.AddUpdateConfiguration(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Enter valid  employer id" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Configuration saved successfully" });

            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
