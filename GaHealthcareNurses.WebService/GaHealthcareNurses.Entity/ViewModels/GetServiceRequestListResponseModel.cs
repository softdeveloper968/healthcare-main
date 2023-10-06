using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class GetServiceRequestListResponseModel
    {
        public int JobId { get; set; }
        public string CareRecipient { get; set; }
        public int Age { get; set; }
        public string ServiceRequired { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TotalHours { get; set; }
        public string Frequency { get; set; }
        public string Location { get; set; }
        public string Agency { get; set; }
        public string CareGiver { get; set; }
        public string Skill { get; set; }
        public string Status { get; set; }
        public string PreferredRate { get; set; }
        public double? ClientRatingToAgency { get; set; }
        public double? ClientRatingToNurse { get; set; }
        public bool IsSupervisoryCreated { get; set; }
        public bool IsInitialAssessmentCreated { get; set; }
        public bool IsCarePlanCreated { get; set; }
        public bool IsDischargeSummaryCreated { get; set; }
    }
}
