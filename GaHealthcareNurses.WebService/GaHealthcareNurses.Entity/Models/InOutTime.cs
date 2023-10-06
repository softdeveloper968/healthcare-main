using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
     public class InOutTime
    {
        public int Id { get; set; }
        public string NurseId { get; set; }
        public DateTime? ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int JobId { get; set;  }
        public virtual Nurse Nurse { get; set; }
        public virtual Job Job { get; set; }
    }
}
