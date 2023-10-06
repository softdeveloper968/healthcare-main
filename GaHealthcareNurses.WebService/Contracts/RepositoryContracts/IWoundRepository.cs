using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IWoundRepository
    {
        Task<List<Wound>> GetByAdultAssessmentId(int adultAssessmentId);
        Task<bool> AddWounds(List<Wound> wounds);
        Task<bool> DeleteWounds(List<Wound> wounds);
    }
}
