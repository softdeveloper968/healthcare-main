using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    public class ServiceAgreement
    {
        public int ServiceAgreementId { get; set; }
        public int JobId { get; set; }
        public string EmployerId { get; set; }
        public string ReferredBy { get; set; }
        public DateTime? ReferredDate { get; set; }
        public DateTime? InitialDateOfContact { get; set; }
        public bool IsMedicaid { get; set; }
        public bool IsInsurance { get; set; }
        public bool IsPrivatePay { get; set; }
        public DateTime? DateBilledOn { get; set; }
        public DateTime DateDueOn { get; set; }
        public int NoOfMonthAgreed { get; set; }
        public string Condition1 { get; set; }
        public string Condition2 { get; set; }
        public int LeastNoOfDays1 { get; set; }
        public int LeastNoOfDays2 { get; set; }
        public bool CanAccessPersonalFunds { get; set; }
        public bool CanAccessPersonalVichle { get; set; }
        public bool HasReceivedBillOfRights { get; set; }
        public bool IsArchived { get; set; }
        public bool IsSignedByClient { get; set; }
        public DateTime? SignedByClient { get; set; }
        public bool IsSignedByAgency { get; set; }
        public DateTime? SignedByAgency { get; set; }
        public virtual Job Job { get; set; }
        public virtual Employer Employer { get; set; }
    }
}
