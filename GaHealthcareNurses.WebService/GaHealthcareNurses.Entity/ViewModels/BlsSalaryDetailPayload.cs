using Newtonsoft.Json;
using System.Collections.Generic;

namespace GaHealthcareNurses.Entity.ViewModels
{
    public class  BlsSalaryDetailPayload
    {
        [JsonProperty(PropertyName = "seriesid")]
        public List<string> SeriesId { get; set; } = new List<string>();
        [JsonProperty(PropertyName = "startyear")]
        public string StartYear { get; set; }
        [JsonProperty(PropertyName = "endyear")]
        public string EndYear { get; set; }
        [JsonProperty(PropertyName = "catalog")]
        public bool Catalog { get; set; }
        [JsonProperty(PropertyName = "calculations")]
        public bool Calculations { get; set; }
        [JsonProperty(PropertyName = "annualaverage")]
        public bool AnnualAverage { get; set; }
        [JsonProperty(PropertyName = "aspects")]
        public bool Aspects { get; set; }
        [JsonProperty(PropertyName = "registrationkey")]
        public string RegistrationKey { get; set; }
    }
}
