using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class AgencyResponseRequestModel
    {
        public int NurseCommentId { get; set; }
        [Required]
        public string AgencyResponse { get; set; }
    }
}
