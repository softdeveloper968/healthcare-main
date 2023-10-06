using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class EmployerViewModel
    {
        public string EmployerId { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public string ServedCounties { get; set; }
        public string TelephoneNo { get; set; }
        public string AgencyWebsite { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public IFormFile Logo { get; set; }
        public int? LPNs { get; set; }
        public int? RNs { get; set; }
        public int? CNAs { get; set; }
        public int? PCAs { get; set; }
    }
}
