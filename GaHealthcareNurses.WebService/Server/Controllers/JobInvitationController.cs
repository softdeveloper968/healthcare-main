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
    [Route("api/")]
    [ApiController]
    public class JobInvitationController : ControllerBase
    {
        private readonly IJobInvitationService _jobInvitationService;
        public JobInvitationController(IJobInvitationService jobInvitationService)
        {
            _jobInvitationService = jobInvitationService;
        }

        // POST: api/SendJobInvitation
        [Route("SendJobInvitation")]
        [HttpPost]
        public async Task<IActionResult> SendInvitation(JobInvitationViewModel model)
        {
            try
            {
                if (model.JobId == 0 || model.Agencies == null || model.Agencies.Count == 0)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Please check details and try again." });

                await _jobInvitationService.SendJobInvitation(model);

                return new JsonResult(new Response<string> { Status = "Success", Message = "Job Invitation is sent to all agencies." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

    }
}
