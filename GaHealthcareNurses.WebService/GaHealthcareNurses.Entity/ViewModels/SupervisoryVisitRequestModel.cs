using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class SupervisoryVisitRequestModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select one option")]
        public string StaffPersonInHome { get; set; }
        [Required(ErrorMessage = "Please select one option")]
        public string SupervisoryVisitInfo { get; set; }
        [Required(ErrorMessage = "Please select one option")]
        public string FollowingPoc { get; set; }
        [Required(ErrorMessage = "Please select one option")]
        public string PocChanged { get; set; }
        [Required(ErrorMessage = "Please select one option")]
        public string PatientCompatible { get; set; }
        [Required(ErrorMessage = "Please select one option")]
        public string CommunicatesWithPt { get; set; }
        [Required(ErrorMessage = "Please select one option")]
        public string CompilesWithInfection { get; set; }
        [Required(ErrorMessage = "Please select one option")]
        public string HonorsPatientRights { get; set; }
        [Required(ErrorMessage = "Please select one option")]
        public string PatientNotifiedOfChange { get; set; }
        [Required]
        public string Details { get; set; }
        public string PatientName { get; set; }
        public string StaffPersonName { get; set; }
    }
    public class SupervisoryVisitPDFBytes
    {
        public byte[] Bytes { get; set; }
    }
}
