using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class ClientRatingViewModel
    {
        public int JobId { get; set; }
        public double?  ClientRatingToAgency { get; set; }
        public double? ClientRatingToNurse { get; set; }
    }
}
