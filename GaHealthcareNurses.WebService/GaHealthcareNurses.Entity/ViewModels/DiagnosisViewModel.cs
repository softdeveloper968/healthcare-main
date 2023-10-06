using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class DiagnosisViewModel
    {
        public int Id { get; set; }
        public int AdultAssessmentId { get; set; }
        [Required]
        public string DiagnosisCode { get; set; }
        public bool IsPrimaryDx { get; set; }
        public bool IsOtherDx { get; set; }
        public string DxDescription { get; set; }
        [Required]
        public int? Rating { get; set; }
        [Required]
        public DateTime? DxDate { get; set; }
    }
}
