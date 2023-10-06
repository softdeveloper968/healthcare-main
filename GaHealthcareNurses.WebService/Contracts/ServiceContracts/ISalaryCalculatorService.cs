using GaHealthcareNurses.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface ISalaryCalculatorService
    {
        Task<List<BlsSalaryDetails>> SearchOccupationsAsync(string occupationCode, double? blsAreaCode);
        Task<List<BlsAreaZipcode>> SearchBlsAreaNamesAsync(string blsAreaName);
        Task<string> GetBlsSeriesId(BlsSalaryDetails blsSalaryDetail);
    }
}
