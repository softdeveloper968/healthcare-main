using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AutoMapper;
using GaHealthcareNurses.Entity.Models;

namespace GaHealthcareNurses.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientProfileController : ControllerBase
    {
        private readonly IClientProfileService _clientProfileService;
        private readonly IMapper _mapper;

        public ClientProfileController(IClientProfileService clientProfileService, IMapper mapper)
        {
            _clientProfileService = clientProfileService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("SaveClientProfile")]
        public async Task<IActionResult> SaveClientProfile([FromBody] ClientProfile clientProfile)
        {
            try
            {
                if (clientProfile == null)
                {
                    return BadRequest("Please provide valid data for Client profile");
                }

                await _clientProfileService.Add(clientProfile);

                return CreatedAtRoute("GetClientProfileById", new { Id = clientProfile.Id }, clientProfile);
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("UpdateClientProfile")]
        public async Task<IActionResult> UpdateClientProfile([FromBody] ClientProfile clientProfile)
        {
            try
            {
                if (clientProfile == null)
                {
                    return BadRequest("Please provide valid data for Client profile");
                }

                ClientProfile responseClientProfile = await _clientProfileService.Update(clientProfile);

                if (responseClientProfile != null)
                    return new JsonResult(new Response<ClientProfile> { Status = "Success", Data = responseClientProfile });

                return new JsonResult(new Response<string> { Status = "Error", Message = "Please check details and try again." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("GetClientProfileList")]
        public async Task<IActionResult> GetClientProfileList()
        {
            try
            {
                List<ClientProfile> lstClientProfile = _mapper.Map<List<ClientProfile>>(await _clientProfileService.Get());
                return new JsonResult(new Response<List<ClientProfile>> { Status = "Success", Data = lstClientProfile });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("GetClientProfileById")]
        public async Task<IActionResult> GetClientProfileById([FromForm]string id)
        {
            try
            {
                ClientProfile clientProfile = await _clientProfileService.GetById(id);
                return new JsonResult(new Response<ClientProfile> { Status = "Success", Data = clientProfile });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
