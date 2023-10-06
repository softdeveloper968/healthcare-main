using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class BlsSalaryDetailsViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public double Id { get; set; }
        [JsonProperty(PropertyName = "series")]
        public string? Series { get; set; } = null;
        [JsonProperty(PropertyName = "areaCode")]
        [Required(ErrorMessage = "Please Select Bls Area Name")]
        [Range(1, double.MaxValue)]
        public double? AreaCode { get; set; } = null;
        [JsonProperty(PropertyName = "areaName")]
        public string? AreaName { get; set; } = null;
        [JsonProperty(PropertyName = "field3")]
        public double Field3 { get; set; }
        [JsonProperty(PropertyName = "occupationCode")]
        [Required(ErrorMessage = "Please Select Bls Occupation Name")]
        [Range(1, double.MaxValue)]
        public double? OccupationCode { get; set; } = null;
        [JsonProperty(PropertyName = "occupationName")]
        public string? OccupationName { get; set; } = null;
        [JsonProperty(PropertyName = "dataType")]
        public double? DataType { get; set; } = null;
        [JsonProperty(PropertyName = "yearPeriod")]
        public string? YearPeriod { get; set; } = null;
        [JsonProperty(PropertyName = "medianSalary")]
        public double MedianSalary { get; set; }
    }
}
