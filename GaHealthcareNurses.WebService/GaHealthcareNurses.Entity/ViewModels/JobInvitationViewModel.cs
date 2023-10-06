using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class JobInvitationViewModel
    {
        public int JobId { get; set; }
        [Required(ErrorMessage = "Select an agency")]
        public List<string> Agencies { get; set; }
    }
}
