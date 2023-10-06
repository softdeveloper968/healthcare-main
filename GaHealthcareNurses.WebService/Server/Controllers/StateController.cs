using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
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
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;
        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }


        // GET: api/State
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var states = await _stateService.Get();

                return new JsonResult(new Response<IEnumerable<State>> { Status = "Success", Data = states });

            }
            catch(Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message=ex.Message });
            }
        }

        // GET: api/State/GetById
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var state = await _stateService.GetById(id);
            if (state == null)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = "Invalid state id" });
            }

            return new JsonResult(new Response<State> { Status = "Success", Data = state });

        }

        [HttpGet]
        [Route("GetCountiesByStateId")]
        public async Task<IActionResult> GetCountiesByStateId(int stateId)
        {
            try
            {
                var counties = await _stateService.GetCountiesByStateId(stateId);

                return new JsonResult(new Response<List<County>> { Status = "Success", Data = counties });

            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

    }
}
