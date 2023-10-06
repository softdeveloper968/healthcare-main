using GaHealthcareNurses.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface ISalaryCalculatorRepository
    {
        Task<List<BlsSalaryDetails>> SearchOccupationsAsync(string occupationName, double? blsAreaCode);
        Task<List<BlsAreaZipcode>> SearchBlsAreaNamesAsync(string blsAreaName);
        Task<BlsSalaryDetails> GetBlsSalaryDetailsAsync(BlsSalaryDetails blsSalaryDetail);
    }
}
