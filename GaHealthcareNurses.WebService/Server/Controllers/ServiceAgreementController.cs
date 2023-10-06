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
    public class ServiceAgreementController : ControllerBase
    {
        private readonly IServiceAgreementService _serviceAgreementService;
        public ServiceAgreementController(IServiceAgreementService serviceAgreementService)
        {
            _serviceAgreementService = serviceAgreementService;
        }

        [HttpGet("GetByEmployerId")]
        public async Task<IActionResult> GetByEmployerId(string employerId)
        {
            try
            {
                if (string.IsNullOrEmpty(employerId))
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Employer is null" });
                var serviceAgreements = await _serviceAgreementService.GetByEmployerId(employerId);
                return new JsonResult(new Response<List<ServiceAgreementListModel>> { Status = "Success", Data = serviceAgreements });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("UpdateServiceAgreement")]
        public async Task<IActionResult> UpdateServiceAgreement(ServiceAgreementRequestModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _serviceAgreementService.UpdateServiceAgreement(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Service agreement not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Service agreement updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var serviceAgreement = await _serviceAgreementService.GetById(id);
                if (serviceAgreement == null)
                    return new JsonResult(new Response<ServiceAgreementRequestModel> { Status = "Error", Message = "Please enter valid Service Agreement id" });
                return new JsonResult(new Response<ServiceAgreementRequestModel> { Status = "Success", Data = serviceAgreement });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("ArchieveServiceAgreement")]
        public async Task<IActionResult> ArchieveServiceAgreement(ArchieveServiceAgreementViewModel model)
        {
            try
            {
                if (model == null)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Model is null" });

                var result = await _serviceAgreementService.ArchieveServiceAgreement(model);
                if (!result)
                    return new JsonResult(new Response<string> { Status = "Error", Message = "Service agreement not found" });
                return new JsonResult(new Response<string> { Status = "Success", Message = "Service agreement updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("GetServiceAgreementPDFBytes/{id}")]
        public async Task<IActionResult> GetServiceAgreementPDFBytes(int id)
        {
            try
            {
                return new JsonResult(await _serviceAgreementService.GetServiceAgreementPDFBytes(id));
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("SendServiceAgreementPDF/{id}")]
        public async Task<IActionResult> SendServiceAgreementPDF(int id)
        {
            try
            {
                return new JsonResult(await _serviceAgreementService.SendServiceAgreementPDF(id));
            }
            catch (Exception ex)
            {
                return new JsonResult(await _serviceAgreementService.SendServiceAgreementPDF(id));
            }
        }


    }
}
