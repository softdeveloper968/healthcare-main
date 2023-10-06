using Newtonsoft.Json;
using System.Collections.Generic;

namespace GaHealthcareNurses.Entity.ViewModels
{
    [JsonObject(Title = "Root")]
    public class Root
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "responseTime")]
        public int ResponseTime { get; set; }
        [JsonProperty(PropertyName = "message")]
        public List<object> Message { get; set; }
        [JsonProperty(PropertyName = "Results")]
        public Results Results { get; set; }
    }
    [JsonObject(Title = "Catalog")]
    public class Catalog
    {
        [JsonProperty(PropertyName = "series_title")]
        public string SeriesTitle { get; set; }
        [JsonProperty(PropertyName = "series_id")]
        public string SeriesId { get; set; }
        [JsonProperty(PropertyName = "seasonality")]
        public string Seasonality { get; set; }
        [JsonProperty(PropertyName = "survey_name")]
        public string SurveyName { get; set; }
        [JsonProperty(PropertyName = "survey_abbreviation")]
        public string SurveyAbbreviation { get; set; }
        [JsonProperty(PropertyName = "measure_data_type")]
        public string MeasureDataType { get; set; }
        [JsonProperty(PropertyName = "commerce_industry")]
        public string CommerceIndustry { get; set; }
        [JsonProperty(PropertyName = "occupation")]
        public string Occupation { get; set; }
        [JsonProperty(PropertyName = "area")]
        public string Area { get; set; }
        [JsonProperty(PropertyName = "area_type")]
        public string AreaType { get; set; }
    }
    [JsonObject(Title = "Data")]
    public class Data
    {
        [JsonProperty(PropertyName = "year")]
        public string Year { get; set; }
        [JsonProperty(PropertyName = "period")]
        public string Period { get; set; }
        [JsonProperty(PropertyName = "periodName")]
        public string PeriodName { get; set; }
        [JsonProperty(PropertyName = "latest")]
        public string Latest { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
        [JsonProperty(PropertyName = "aspects")]
        public List<object> Aspects { get; set; }
        [JsonProperty(PropertyName = "footnotes")]
        public List<Footnot> Footnotes { get; set; }
    }
    [JsonObject(Title = "Footnote")]
    public class Footnot
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
    [JsonObject(Title = "Results")]
    public class Results
    {
        [JsonProperty(PropertyName = "series")]
        public List<Series> Series { get; set; }
    }
    [JsonObject(Title = "Series")]
    public class Series
    {
        [JsonProperty(PropertyName = "seriesID")]
        public string SeriesId { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<Data> Data { get; set; }
        [JsonProperty(PropertyName = "catalog")]
        public Catalog Catalog { get; set; }
    }
}
