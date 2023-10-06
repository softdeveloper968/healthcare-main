using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class SalaryCalculatorService: ISalaryCalculatorService
    {
        private readonly ISalaryCalculatorRepository _blsSalaryCalculatorRepository;
        public SalaryCalculatorService(ISalaryCalculatorRepository blsSalaryCalculatorRepository)
        {
            _blsSalaryCalculatorRepository = blsSalaryCalculatorRepository;

        }
        public async Task<string> GetBlsSeriesId(BlsSalaryDetails blsSalaryDetail)
        {
            var blsSalaryDetailModel = await _blsSalaryCalculatorRepository.GetBlsSalaryDetailsAsync(blsSalaryDetail);
            var blsSeriesId = await GenerateBlsSeriesId(blsSalaryDetailModel);
            return blsSeriesId;
        }
        public async Task<List<BlsAreaZipcode>> SearchBlsAreaNamesAsync(string blsAreaName)
        {
            return await _blsSalaryCalculatorRepository.SearchBlsAreaNamesAsync(blsAreaName);
        }
        private async Task<string> GenerateBlsSeriesId(BlsSalaryDetails model)
        {
            int? areaCodeLength = Convert.ToString(model.AreaCode)?.Length;
            string prefixAreaCode = string.Empty;
            switch (areaCodeLength)
            {
                case 5:
                    prefixAreaCode = $"00{model.AreaCode}";
                    break;
                case 6:
                    prefixAreaCode = $"0{model.AreaCode}";
                    break;
                case 7:
                    prefixAreaCode = $"{model.AreaCode}";
                    break;
            }
            var blsSeriesId = $"{model.Series}{prefixAreaCode}000000{model.OccupationCode}0{model.DataType}";
            return await Task.FromResult(blsSeriesId);
        }
        public async Task<List<BlsSalaryDetails>> SearchOccupationsAsync(string occupationName, double? blsAreaCode)
        {
            return await _blsSalaryCalculatorRepository.SearchOccupationsAsync(occupationName, blsAreaCode);
        }
    }
}
