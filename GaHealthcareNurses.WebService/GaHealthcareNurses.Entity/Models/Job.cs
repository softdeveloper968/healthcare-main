using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHealthcareNurses.Entity.Models
{
    public partial class Job
    {
        public Job()
        {
            VisitNotes = new HashSet<VisitNote>();
            JobApplies = new HashSet<JobApply>();
            JobApplyForAgencies = new HashSet<JobApplyForAgency>();
            TaskList = new HashSet<TaskList>();
            DischargeSummaries = new HashSet<DischargeSummary>();
            CarePlans = new HashSet<CarePlan>();
            SupervisoryVisits = new HashSet<SupervisoryVisit>();
            AdultAssessments = new HashSet<AdultAssessment>();
            InOutTimes = new HashSet<InOutTime>();
        }
        public int JobId { get; set; }

        [ForeignKey("Employer")]
        public string EmployerId { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public int CareRecipientId { get; set; }
        public int? JobTitleId { get; set; }
    //    public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; }
        public double HourlyRate { get; set; }
        public bool SentToNurse { get; set; }
        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }
     //   public int CityId { get; set; }
        public int? ResourceId { get; set; }
        public double? ClientRatingToAgency { get; set; }
        public double? ClientRatingToNurse { get; set; }
        public string ClientPaymentMethod { get; set; }
        public bool IsDischargeSummaryRequired { get; set; }
        public bool IsInitialAssesstmentRequired { get; set; }
        public bool IsSupervisoryVisitsRequired { get; set; }
        public int? ReAssessmentFrequency { get; set; }
        public int? ReVisitFrequency { get; set; }
        public int? CareGiverRate { get; set; }
        public virtual Employer Employer { get; set; }
        public virtual Client Client { get; set; }
    //    public virtual City City { get; set; }
        public virtual Resource Resource { get; set; }
        public virtual CareRecipient CareRecipient { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual ICollection<VisitNote> VisitNotes { get; set; }
        public virtual ICollection<JobApply> JobApplies { get; set; }
        public virtual ICollection<JobApplyForAgency> JobApplyForAgencies { get; set; }
        public virtual ICollection<TaskList> TaskList { get; set; }
        public virtual ICollection<DischargeSummary> DischargeSummaries { get; set; }
        public virtual ICollection<CarePlan> CarePlans { get; set; }
        public virtual ICollection<SupervisoryVisit> SupervisoryVisits { get; set; }
        public virtual ICollection<AdultAssessment> AdultAssessments { get; set; }
        public virtual ICollection<InOutTime> InOutTimes { get; set; }

    }
}

