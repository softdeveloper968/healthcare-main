using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface IWoundService
    {
        Task<List<Wound>> GetByAdultAssessmentId(int adultAssessmentId);
        Task<bool> AddWounds(List<Wound> wounds);
        Task<bool> DeleteWounds(List<Wound> wounds);
    }
}
