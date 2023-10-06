using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHealthcareNurses.Entity.Models
{
    public partial class Employer
    {
        public Employer()
        {
          //  Jobs = new HashSet<Job>();
            VisitNotes = new HashSet<VisitNote>();
            AgencyServedCounties = new HashSet<AgencyServedCounties>();
            Nurses = new HashSet<Nurse>();
        }

        [Key]
        [ForeignKey("IdentityUser")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public string TelephoneNo { get; set; }
        public string AgencyWebsite { get; set; }
        public string EmailAddress { get; set; }
        public string Logo { get; set; }
        public int? SubscriptionId { get; set; }
        public bool IsSubscriptionActive { get; set; }
        public bool IsSubscriptionRecurring { get; set; }
        public bool IsDeleted { get; set; }
        public bool NursesList { get; set; }
        public bool Tasks { get; set; }
        public bool Documents { get; set; }
        public bool HeatMap { get; set; }
        public bool InitialAssessment { get; set; }
        public bool SupervisoryVisits { get; set; }
        public bool DischargeSummary { get; set; }
        public bool CarePlan { get; set; }
        public bool CareRecipients { get; set; }
        public bool ServiceAgreements { get; set; }
        public bool Calendar { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public int? LPNs { get; set; }
        public int? RNs { get; set; }
        public int? CNAs { get; set; }
        public int? PCAs { get; set; }
        //   public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<VisitNote> VisitNotes { get; set; }
        public virtual ICollection<AgencyServedCounties> AgencyServedCounties { get; set; }
        public virtual ICollection<Nurse> Nurses { get; set; }
    }
}

