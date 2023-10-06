using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class UpdateNurseStatusViewModel
    {
        public string NurseId { get; set; }
        public bool IsInactive { get; set; }
    }
}
