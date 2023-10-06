using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
  public class UploadDocuments
    {
        [Key]
        [ForeignKey("Nurse")]
        public string Id { get; set; }
        public string CnaOrProfessionalLicense { get; set; }
        public DateTime? CnaExpiryDate { get; set; }
        public string CprProvideNewLicense { get; set; }
        public DateTime? CprExpiryDate { get; set; }
        public string Ssn { get; set; }
        public DateTime? SsnExpiryDate { get; set; }
        public string DrivingLicense { get; set; }
        public DateTime? DrivingLicenseExpiryDate { get; set; }
        public string TbTestResults { get; set; }
        public DateTime? TbTestResultsExpiryDate { get; set; }
        public string StaffPortalInfo { get; set; }
        public DateTime? StaffPortalExpiryDate { get; set; }
        public string PcaTest { get; set; }
        public DateTime? PcaTestExpiryDate { get; set; }
        public string HiringDisclosures { get; set; }
        public DateTime? HiringDisclosuresExpiryDate { get; set; }
        public string HiringPreScreening { get; set; }
        public DateTime? HiringPreScreeningExpiryDate { get; set; }
        public string OfficeWillPulled { get; set; }
        public DateTime? OfficeWillPulledExpiryDate { get; set; }
        public string SexOffenderReportOfficeWillPulled { get; set; }
        public DateTime? SexOffenderReportOfficeWillPulledExpiryDate { get; set; }
        public string EVerifyWillPulled { get; set; }
        public DateTime? EVerifyWillPulledExpiryDate { get; set; }
        public string BackgroundCheck { get; set; }
        public DateTime? BackgroundCheckExpiryDate { get; set; }
        public virtual Nurse Nurse { get; set; }

    }
    public class FileData
    {
        public byte[] Data { get; set; }
        public string FileType { get; set; }
        public long Size { get; set; }

    }
}
