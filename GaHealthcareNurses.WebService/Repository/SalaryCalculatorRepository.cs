using System.Collections.Generic;
using GaHealthcareNurses.Entity;
using GaHealthcareNurses.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Contracts.RepositoryContracts;

namespace Repository
{
    public class SalaryCalculatorRepository:ISalaryCalculatorRepository
    {
        private GaHealthcareNursesContext _gaHealthcareNursesContext;
        public SalaryCalculatorRepository(GaHealthcareNursesContext gaHealthcareNursesContext)
        {
            _gaHealthcareNursesContext = gaHealthcareNursesContext;
        }
        public async Task<List<BlsSalaryDetails>> SearchOccupationsAsync(string? occupationName = null, double? blsAreaCode = null)
        {
            if (!string.IsNullOrEmpty(occupationName) && blsAreaCode != null)
            {
                return await _gaHealthcareNursesContext.BlsSalaryDetails.Where(x => x.OccupationName.Contains(occupationName) && x.AreaCode == blsAreaCode).Select(x => new BlsSalaryDetails() { OccupationName = x.OccupationName, OccupationCode = x.OccupationCode, AreaCode = x.AreaCode }).ToListAsync();
            }
            else if(!string.IsNullOrEmpty(occupationName) && blsAreaCode == null)
            {
                return await _gaHealthcareNursesContext.BlsSalaryDetails.Where(x => x.OccupationName.Contains(occupationName)).Select(x => new BlsSalaryDetails() { OccupationName = x.OccupationName, OccupationCode = x.OccupationCode, AreaCode = x.AreaCode }).ToListAsync();
            }
            return new List<BlsSalaryDetails>();
        }
        public async Task<List<BlsAreaZipcode>> SearchBlsAreaNamesAsync(string blsAreaName)
        {
            if (!string.IsNullOrEmpty(blsAreaName))
            {
                return await _gaHealthcareNursesContext.BlsAreaZipcode.Where(x => x.BlsAreaName.Contains(blsAreaName)).Select(x => new BlsAreaZipcode() { BlsAreaName = x.BlsAreaName, BlsAreaCode = x.BlsAreaCode }).ToListAsync();
            }
            return new List<BlsAreaZipcode>();
        }
        public async Task<BlsSalaryDetails> GetBlsSalaryDetailsAsync(BlsSalaryDetails blsSalaryDetail)
        {
            var blsSalaryDetailModel = _gaHealthcareNursesContext.BlsSalaryDetails.Where(x => x.AreaCode == blsSalaryDetail.AreaCode && x.OccupationCode == blsSalaryDetail.OccupationCode).Select(x => new BlsSalaryDetails { AreaCode = x.AreaCode, Series = x.Series, DataType = x.DataType, OccupationCode = x.OccupationCode }).FirstOrDefault();
            return await Task.FromResult(blsSalaryDetailModel ?? new BlsSalaryDetails());
        }
    }
}
