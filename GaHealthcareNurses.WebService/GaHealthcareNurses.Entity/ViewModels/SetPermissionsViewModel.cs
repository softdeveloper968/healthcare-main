using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class SetPermissionsViewModel
    {
        public string EmployerId { get; set; }
        public string Name { get; set; }
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
    }
}
