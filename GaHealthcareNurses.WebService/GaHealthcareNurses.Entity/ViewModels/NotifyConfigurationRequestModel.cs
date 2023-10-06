using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class NotifyConfigurationRequestModel
    {
        public string EmployerId { get; set; }
        [Required]
        public int? DaysBeforeDocExpires { get; set; }
        [Required]
        public double? HoursFrequencyForDocs { get; set; }
        public bool NotifyDocExpByApp { get; set; }
        public bool NotifyDocExpByText { get; set; }
        public bool NotifyDocExpByEmail { get; set; }
        [Required]
        public int? MinutesBeforeClockIn { get; set; }
        [Required]
        public int? MinutesFreqToRepeat { get; set; }
        [Required]
        public int? MinutesAfterShiftEnd { get; set; }
    }
}
