using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface INurseInboxRepository
    {
        Task<bool> AddMessage(NurseInbox model);
        Task<List<NurseInbox>> GetNurseMessages(string nurseId);
        Task<NurseInbox> GetById(int id);
        Task<bool> UpdateMessage(NurseInbox model);
        Task<bool> DeleteMessage(NurseInbox model);
    }
}
