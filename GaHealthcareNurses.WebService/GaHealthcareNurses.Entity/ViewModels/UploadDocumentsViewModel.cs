using Microsoft.AspNetCore.Http;
using System;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class UploadDocumentsViewModel
    {
        public string Id { get; set; }
        public IFormFile CnaOrProfessionalLicense { get; set; }
        public string CnaExpiryDate { get; set; }
        public IFormFile CprProvideNewLicense { get; set; }
        public string CprExpiryDate { get; set; }
        public IFormFile Ssn { get; set; }
        public string SsnExpiryDate { get; set; }
        public IFormFile DrivingLicense { get; set; }
        public string DrivingLicenseExpiryDate { get; set; }
        public IFormFile TbTestResults { get; set; }
        public string TbTestResultsExpiryDate { get; set; }
        public IFormFile StaffPortalInfo { get; set; }
        public string StaffPortalExpiryDate { get; set; }
        public IFormFile PcaTest { get; set; }
        public string PcaTestExpiryDate { get; set; }
        public IFormFile HiringDisclosures { get; set; }
        public string HiringDisclosuresExpiryDate { get; set; }
        public IFormFile HiringPreScreening { get; set; }
        public string HiringPreScreeningExpiryDate { get; set; }
        public IFormFile OfficeWillPulled { get; set; }
        public string OfficeWillPulledExpiryDate { get; set; }
        public IFormFile SexOffenderReportOfficeWillPulled { get; set; }
        public string SexOffenderReportOfficeWillPulledExpiryDate { get; set; }
        public IFormFile EVerifyWillPulled { get; set; }
        public string EVerifyWillPulledExpiryDate { get; set; }
        public IFormFile BackgroundCheck { get; set; }
        public string BackgroundCheckExpiryDate { get; set; }
    }
}
