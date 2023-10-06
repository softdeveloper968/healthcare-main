using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaHealthcareNurses.Client.Services
{
    public class ServiceRequestNotificationService : IDisposable
    {
        public event Action<int> GetNotificationCount;

        public void GetServiceRequestNotifications(int notificationCount)
        {
            GetNotificationCount?.Invoke(notificationCount);
        }
        public void Dispose()
        {
        }
    }
}
