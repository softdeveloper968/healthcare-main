using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    public class NotifyConfiguration
    {
        public int NotifyConfigurationId { get; set; }
        public string EmployerId { get; set; }
        public int DaysBeforeDocExpires { get; set; }
        public double HoursFrequencyForDocs { get; set; }
        public bool NotifyDocExpByApp { get; set; }
        public bool NotifyDocExpByText { get; set; }
        public bool NotifyDocExpByEmail { get; set; }
        public int MinutesBeforeClockIn { get; set; }
        public int MinutesFreqToRepeat { get; set; }
        public int MinutesAfterShiftEnd { get; set; }
        public virtual Employer Employer { get; set; }
    }
}
