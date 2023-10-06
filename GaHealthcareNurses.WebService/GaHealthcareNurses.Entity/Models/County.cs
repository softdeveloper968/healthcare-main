using System;
using System.Collections.Generic;
using System.Text;

namespace GaHealthcareNurses.Entity.Models
{
    public class County
    {
        public int Id { get; set; }
        public string CountyName { get; set; }
        public decimal? CountyPopulation { get; set; }
        public int? StateId { get; set; }
        public virtual State State { get; set; }
    }
}
