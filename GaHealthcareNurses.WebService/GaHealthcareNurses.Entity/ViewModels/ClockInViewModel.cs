using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class ClockInViewModel
    {
        public int JobId { get; set; }
        public string NurseId { get; set; }
        public DateTime ClockInTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
