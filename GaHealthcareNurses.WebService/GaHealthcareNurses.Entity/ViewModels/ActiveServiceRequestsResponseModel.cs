using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class ActiveServiceRequestsResponseModel
    {
        public int JobId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsClockedIn { get; set; }
        public bool IsClockedOut { get; set; }
    }
}
