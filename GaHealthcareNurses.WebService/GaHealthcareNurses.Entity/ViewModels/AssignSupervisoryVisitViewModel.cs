using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class AssignSupervisoryVisitViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select a nurse")]
        public string NurseId { get; set; }
    }
}
