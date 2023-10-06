using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
   public class AddCommentViewModel
    {
        public string NurseId { get; set; }
        public int JobId { get; set; }
        public string Comment { get; set; }
        public bool IsUrgent { get; set; }
    }
}
