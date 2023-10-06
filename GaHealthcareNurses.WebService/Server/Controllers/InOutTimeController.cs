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
    public class InOutTimeController : ControllerBase
    {
        private readonly IInOutTimeService _inOutTimeService;

        public InOutTimeController(IInOutTimeService inOutTimeService)
        {
            _inOutTimeService = inOutTimeService;
        }

        [HttpPost("ClockIn")] 
        public async Task<IActionResult> ClockIn(ClockInViewModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _inOutTimeService.ClockIn(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Nurse not Clock In that Day" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Nurse successfully Web-Clock In" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("ClockOut")]
        public async Task<IActionResult> ClockOut(ClockOutViewModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _inOutTimeService.ClockOut(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "There Is No Entery For Clock In " });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Nurse successfully Web-Clock Out" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
