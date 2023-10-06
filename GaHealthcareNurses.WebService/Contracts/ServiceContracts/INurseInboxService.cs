using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface INurseInboxService
    {
        Task<bool> AddMessage(NurseInbox model);
        Task<List<GetNurseMessagesResponseModel>> GetNurseMessages(string nurseId);
        Task<bool> ReadMessage(ReadMessageViewModel model);
        Task<bool> DeleteMessage(int id);
    }
}
