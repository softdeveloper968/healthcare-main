using GaHealthcareNurses.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
   public class JobResponseModel
    {
        public int JobId { get; set; }
        public string EmployerId { get; set; }
        public string ClientId { get; set; }
        public int CareRecipientId { get; set; }
        public int? JobTitleId { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; }
        public double HourlyRate { get; set; }
        public bool SentToNurse { get; set; }
        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }
        public int? ResourceId { get; set; }
        public double? ClientRatingToAgency { get; set; }
        public double? ClientRatingToNurse { get; set; }
        public List<string> Tasks { get; set; }
        public Employer Employer { get; set; }
        public Client Client { get; set; }
        public Resource Resource { get; set; }
        public CareRecipient CareRecipient { get; set; }
        public JobTitle JobTitle { get; set; }
        public ICollection<VisitNote> VisitNotes { get; set; }
        public ICollection<JobApply> JobApplies { get; set; }
        //public IEnumerable<TaskList> TaskList { get; set; }
    }
}
