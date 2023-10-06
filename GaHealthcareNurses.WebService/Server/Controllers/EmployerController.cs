using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity;
using GaHealthcareNurses.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using GaHealthcareNurses.WebService.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using AutoMapper;
using GaHealthcareNurses.Entity.ViewModels;
using System.Linq;

namespace GaHealthcareNurses.WebService.Controllers
{
    // [Authorize(Roles = "Employer")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _employerService;
        private readonly GaHealthcareNursesContext _gaHealthcareNursesContext;
        private readonly ILogger<EmployerController> _logger;
        private readonly IMapper _mapper;
        public EmployerController(IEmployerService employerService, ILogger<EmployerController> logger, GaHealthcareNursesContext gaHealthcareNursesContext, IMapper mapper)
        {
            _employerService = employerService;
            _logger = logger;
            _gaHealthcareNursesContext = gaHealthcareNursesContext;
            _mapper = mapper;
        }


        // GET: api/Employer
        [HttpGet]
        public async Task<object> GetAll()
        {
            int count = await _employerService.TotalCount(string.Empty);
            var queryString = Request.Query;
            string sort = queryString["$orderby"];   //sorting      
            string filter = queryString["$filter"];
            string auto = queryString["$inlineCount"];

            StringValues Skip;
            StringValues Take;
            StringValues Filter;
            int skip = (queryString.TryGetValue("$skip", out Skip)) ? Convert.ToInt32(Skip[0]) : 0;
            string filterValue = (queryString.TryGetValue("$filter", out Filter)) ? Convert.ToString(Filter[0])?.Split('\'')[1] : null;
            if (filterValue != null)
            {
                count = await _employerService.TotalCount(filterValue);
            }
            int top = (queryString.TryGetValue("$top", out Take)) ? Convert.ToInt32(Take[0]) : count;


            var employers = await _employerService.Get(skip, top, filterValue);
            if (employers == null)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = "Please check details and try again." });
            }
            return new { Items = employers, Count = count };

        }

        // GET: api/Employer/GetAgenciesList
        [HttpGet]
        [Route("GetAgenciesList")]
        public async Task<IActionResult> GetAgenciesList()
        {
            try
            {
                var agencies = _mapper.Map<List<GetAgencyListResponseModel>>(await _employerService.GetAll());
                return new JsonResult(new Response<List<GetAgencyListResponseModel>> { Status = "Success", Data = agencies });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }


        // GET: api/Employer/GetById
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var employer = await _employerService.GetById(id);
            if (employer == null)
                return new JsonResult(new Response<string> { Status = "Error", Message = "Invalid employer id" });

            return new JsonResult(new Response<Employer> { Status = "Success", Data = employer });
        }


        // POST: api/Employer
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employer employer)
        {
            if (employer == null)
            {
                return BadRequest("Employer is null");
            }
            await _employerService.Add(employer);
            return CreatedAtRoute(
                "Get", new
                {
                    Id = employer.Id
                }, employer);
        }


        // DELETE: api/Employer/1
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _employerService.Delete(id);
                if (result)
                    return new JsonResult(new Response<string> { Status = "Success", Message = "Agency deleted successfully!" });

                return new JsonResult(new Response<string> { Status = "Error", Message = "Agency not found" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }


        // PUT: api/Employer/Update
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromForm] EmployerViewModel employer)
        {
            try
            {
                if (employer == null)
                {
                    return BadRequest("Employer is null.");
                }

                var employerUpdatedData = await _employerService.Update(employer);
                if (employerUpdatedData != null)
                    return new JsonResult(new Response<Employer> { Status = "Success", Data = employerUpdatedData });

                return new JsonResult(new Response<string> { Status = "Error", Message = "Please check details and try again." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }

        }


        // GET: api/Employer/GetAgenciesForBid
        [HttpGet]
        [Route("GetAgenciesForBid")]
        public async Task<IActionResult> GetAgenciesForBid(int jobId)
        {
            try
            {
                var agencies = await _employerService.GetAgenciesForBid(jobId);
                return new JsonResult(new Response<List<Employer>> { Status = "Success", Data = agencies });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        // GET: api/Employer/GetAgencyServedCounties
        [HttpGet]
        [Route("GetAgencyServedCounties")]
        public async Task<IActionResult> GetAgencyServedCounties(string employerId)
        {
            try
            {
                var servedCounties = await _employerService.GetAgencyServedCounties(employerId);
                return new JsonResult(new Response<List<AgencyServedCounties>> { Status = "Success", Data = servedCounties });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("SetPermissions")]
        public async Task<IActionResult> SetPermissions(SetPermissionsViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Model is null.");
                }
                var result = await _employerService.SetPermissions(model);
                if (result)
                    return new JsonResult(new Response<string> { Status = "Success", Message = "Permissions set successfully!" });

                return new JsonResult(new Response<string> { Status = "Error", Message = "Agency not found" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetNursesList")]
        public async Task<IActionResult> GetNursesList(string employerId)
        {
            try
            {
                var nurses = await _employerService.GetNursesList(employerId);
                return new JsonResult(new Response<List<Nurse>> { Status = "Success", Data = nurses });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
