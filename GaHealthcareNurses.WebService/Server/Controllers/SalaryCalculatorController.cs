using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GaHealthcareNurses.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryCalculatorController : ControllerBase
    {
        private readonly ISalaryCalculatorService _salaryCalculatorService;
        public SalaryCalculatorController(ISalaryCalculatorService salaryCalculatorService)
        {
            _salaryCalculatorService = salaryCalculatorService;
        }

        [HttpPost("CalculateBlsSalaryAsync")]
        public async Task<IActionResult> CalculateBlsSalaryAsync(BlsSalaryDetailsViewModel blsSalaryDetailViewModel)
        {
            try
            {
                if (blsSalaryDetailViewModel is null)
                {
                    return BadRequest();
                }
                BlsSalaryDetails blsSurveyModel = new BlsSalaryDetails()
                {
                    AreaCode = Convert.ToDouble(blsSalaryDetailViewModel.AreaCode),
                    OccupationCode = Convert.ToDouble(blsSalaryDetailViewModel.OccupationCode)
                };
                var blsSeriesId = await _salaryCalculatorService.GetBlsSeriesId(blsSurveyModel);
                var payload = new BlsSalaryDetailPayload
                {
                    SeriesId = { blsSeriesId },
                    StartYear = "2021",
                    EndYear = "2021",
                    Catalog = false,
                    Calculations = false,
                    AnnualAverage = false,
                    Aspects = false,
                    RegistrationKey = "9133bbce9e3e484b9b75ec3b84a8afa3"
                };
                var stringPayload = JsonConvert.SerializeObject(payload);
                HttpClient httpClient = new HttpClient();
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                var httpResponse = await httpClient.PostAsync("https://api.bls.gov/publicAPI/v2/timeseries/data.csv", httpContent);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = httpResponse.Content.ReadAsStringAsync().Result;
                    Root? root = JsonConvert.DeserializeObject<Root>(result);
                    if (!string.IsNullOrEmpty(result))
                    {
                        var blsSalary = root?.Results.Series.FirstOrDefault()?.Data.FirstOrDefault()?.Value;
                        if (!string.IsNullOrEmpty(blsSalary) && !blsSalary.Contains("-"))
                        {
                            return new JsonResult(new Response<string> { Status = "Success", Data = root?.Results?.Series?.FirstOrDefault()?.Data?.FirstOrDefault()?.Value });
                        }
                        else
                        {
                            return new JsonResult(new Response<string> { Status = "Error", Message = root?.Results?.Series?.FirstOrDefault()?.Data?.FirstOrDefault()?.Footnotes?.FirstOrDefault()?.Text });
                        }
                    }
                    else
                    {
                        return new JsonResult(new Response<string> { Status = "Error", Message = "No data found" });
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("SearchOccupationsAsync")]
        public async Task<IActionResult> SearchOccupationsAsync(string? occupationName = null, double? blsAreaCode = null)
        {
            try
            {
                List<BlsSalaryDetails> blsOccupationsList = await _salaryCalculatorService.SearchOccupationsAsync(occupationName ?? string.Empty, blsAreaCode);
                if (blsOccupationsList is null)
                {
                    return new JsonResult(new Response<List<BlsSalaryDetails>> { Status = "Success", Data = blsOccupationsList });
                }
                return new JsonResult(new Response<List<BlsSalaryDetails>> { Status = "Success", Data = blsOccupationsList });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("SearchBlsAreaNamesAsync")]
        public async Task<IActionResult> SearchBlsAreaNamesAsync(string blsAreaName)
        {
            try
            {
                List<BlsAreaZipcode> blsAreaNamesList = await _salaryCalculatorService.SearchBlsAreaNamesAsync(blsAreaName ?? String.Empty);
                if (blsAreaNamesList is null)
                {
                    return new JsonResult(new Response<List<BlsAreaZipcode>> { Status = "Success", Data = new List<BlsAreaZipcode>() });
                }
                return new JsonResult(new Response<List<BlsAreaZipcode>> { Status = "Success", Data = blsAreaNamesList });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string> { Status = "Error", Message = ex.Message });
            }
        }
    }
}
