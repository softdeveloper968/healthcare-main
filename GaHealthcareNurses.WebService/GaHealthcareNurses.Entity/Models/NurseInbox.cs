using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    public class NurseInbox
    {
        public int Id { get; set; }
        public string NurseId { get; set; }
        public string EmployerId { get; set; }
        public int JobId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentDate { get; set; }
        public virtual Nurse Nurse { get; set; }
        public virtual Employer Employer { get; set; }
        public virtual Job Job { get; set; }
    }
}
