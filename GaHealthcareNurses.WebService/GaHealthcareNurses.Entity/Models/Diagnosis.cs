using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    public class Diagnosis
    {
        public int DiagnosisId { get; set; }
        public int? AdultAssessmentId { get; set; }
        public string DiagnosisCode { get; set; }
        public bool IsPrimaryDx { get; set; }
        public bool IsOtherDx { get; set; }
        public string DxDescription { get; set; }
        public int Rating { get; set; }
        public DateTime DxDate { get; set; }
        public virtual AdultAssessment AdultAssessment { get; set; }
    }
}
