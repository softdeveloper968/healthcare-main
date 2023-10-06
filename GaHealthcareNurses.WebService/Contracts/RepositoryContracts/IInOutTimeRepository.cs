using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
     public interface IInOutTimeRepository
    {
        Task<bool> AddInOutTime(InOutTime model);
        Task<bool> UpdateInOutTime(InOutTime model);
        Task<InOutTime> GetInOutTimeById(int id);
        Task<InOutTime> GetInOutTimeForDay(DateTime date, int jobId);
    }
}
