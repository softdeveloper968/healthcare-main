using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    [Keyless]
    public class BlsAreaZipcode
    {
        public double BlsAreaCode { get; set; }
        public string BlsAreaName { get; set; }
        public double ZipCode { get; set; }
    }
}
