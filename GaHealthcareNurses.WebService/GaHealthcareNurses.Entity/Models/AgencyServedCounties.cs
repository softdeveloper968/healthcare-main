using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    public class AgencyServedCounties
    {
        public int Id { get; set; }
        public string EmployerId { get; set; }
        public int CountyId { get; set; }
        public virtual Employer Employer { get; set; }
        public virtual County County { get; set; }
    }
}
