using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class UpdateStatusVisibilityViewModel
    {
        public int CareRecipientId { get; set; }
        [Required(ErrorMessage ="Please select a status")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Please select a visibility")]
        public string Visibility { get; set; }
    }
}
