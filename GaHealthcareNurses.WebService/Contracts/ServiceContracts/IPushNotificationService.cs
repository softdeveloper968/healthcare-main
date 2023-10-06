using GaHealthcareNurses.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.ServiceContracts
{
    public interface IPushNotificationService
    {
        Task<bool> SendNotification(object data);
        Task SendNotificationToAllNurses(List<Nurse> nurses, object data);
    }
}
