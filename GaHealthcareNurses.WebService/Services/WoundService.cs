using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WoundService : IWoundService
    {
        private IWoundRepository _woundRepository;

        #region Contructor For WoundService
        public WoundService(IWoundRepository woundRepository)
        {
            _woundRepository = woundRepository;
        }
        #endregion

        #region Implementing Interface
        public async Task<bool> AddWounds(List<Wound> wounds)
        {
            return await _woundRepository.AddWounds(wounds);
        }

        public async Task<bool> DeleteWounds(List<Wound> wounds)
        {
            return await _woundRepository.DeleteWounds(wounds);
        }

        public async Task<List<Wound>> GetByAdultAssessmentId(int adultAssessmentId)
        {
            return await _woundRepository.GetByAdultAssessmentId(adultAssessmentId);
        }
        #endregion
    }
}
