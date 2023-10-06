using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    [Keyless]
    public class BlsSalaryDetails
    {
        public double Id { get; set; }
        public string Series { get; set; }
        public double AreaCode { get; set; }
        public string AreaName { get; set; }
        public double Field3 { get; set; }
        public double? OccupationCode { get; set; }
        public string OccupationName { get; set; }
        public double? DataType { get; set; }
        public string YearPeriod { get; set; }
        public double MedianSalary { get; set; }
    }
}
