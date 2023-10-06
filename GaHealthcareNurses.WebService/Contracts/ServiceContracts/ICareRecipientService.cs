using GaHealthcareNurses.Entity.Models;
using GaHealthcareNurses.Entity.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface ICareRecipientService
    {
        Task<IEnumerable<CareRecipient>> Get();
        Task<CareRecipient> GetById(int id);
        Task<int> Add(CareRecipient careRecipient);
        Task Delete(CareRecipient careRecipient);
        Task Update(CareRecipient careRecipient);
        Task SendCEUEmail(CEUViewModel cEUViewModel);
        Task<bool> UpdateStatusAndVisibility(UpdateStatusVisibilityViewModel model);
    }
}
