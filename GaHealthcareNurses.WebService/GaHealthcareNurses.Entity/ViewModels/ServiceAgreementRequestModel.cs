using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class ServiceAgreementRequestModel
    {
        public int ServiceAgreementId { get; set; }
        [Required]
        public string ReferredBy { get; set; }
        [Required]
        public DateTime? ReferredDate { get; set; }
        [Required]
        public DateTime? InitialDateOfContact { get; set; }
        public bool IsMedicaid { get; set; }
        public bool IsInsurance { get; set; }
        public bool IsPrivatePay { get; set; }
        [Required]
        public DateTime? DateBilledOn { get; set; }
        [Required]
        public DateTime? DateDueOn { get; set; }
        [Required]
        public int NoOfMonthAgreed { get; set; }
        public string Condition1 { get; set; }
        public string Condition2 { get; set; }
        public int LeastNoOfDays1 { get; set; }
        public int LeastNoOfDays2 { get; set; }
        public bool CanAccessPersonalFunds { get; set; }
        public bool CanAccessPersonalVichle { get; set; }
        public bool HasReceivedBillOfRights { get; set; }
        public bool IsSignedByClient { get; set; }
        public DateTime? SignedByClient { get; set; }
        public bool IsSignedByAgency { get; set; }
        public DateTime? SignedByAgency { get; set; }

        public string ClientName { get; set; }
        public string DescriptionByClient { get; set; }
        public string ServiceProvide { get; set;  }
        public string TotalHours { get; set; }
        public string Frequency { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ChargesOfService { get; set; }
        public string AgencyName { get; set; }
        public string AgencyPhoneNo { get; set; }
    }
    public class ServiceAgreementPDFBytes
    {
        public byte[] Bytes { get; set; }
    }
}
