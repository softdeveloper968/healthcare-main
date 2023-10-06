using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    public class SupervisoryVisit
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string NurseId { get; set; }
        public string EmployerId { get; set; }
        public string StaffPersonInHome { get; set; }
        public string SupervisoryVisitInfo { get; set; }
        public string FollowingPoc { get; set; }
        public string PocChanged { get; set; }
        public string PatientCompatible { get; set; }
        public string CommunicatesWithPt { get; set; }
        public string CompilesWithInfection { get; set; }
        public string HonorsPatientRights { get; set; }
        public string PatientNotifiedOfChange { get; set; }
        public string Details { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Job Job { get; set; }
        public virtual Nurse Nurse { get; set; }
        public virtual Employer Employer { get; set; }
    }
}
