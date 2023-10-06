using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class ClockOutViewModel
    {
        public int JobId { get; set; }
        public string NurseId { get; set; }
        public DateTime ClockOutTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
