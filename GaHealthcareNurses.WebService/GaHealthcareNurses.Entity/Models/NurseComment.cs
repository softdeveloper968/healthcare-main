using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
   public class NurseComment
    {
        public int NurseCommentId { get; set; }
        public string NurseId { get; set; }
        public int JobId { get; set; }
        public string EmployerId { get; set; }
        public string Comment { get; set; }
        public string AgencyResponse { get; set; }
        public DateTime CommentDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsRead { get; set; }
        public bool IsArchived { get; set; }
        public virtual Nurse Nurse { get; set; }
        public virtual Job Job { get; set; }
        public virtual Employer Employer { get; set; }
    }
}
