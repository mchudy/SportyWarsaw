using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SportyWarsaw.Domain.Enums;

namespace SportyWarsaw.WebApi.Models
{
    public class SportsFacilityModel
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string District { get; set; }


        [JsonConverter(typeof(StringEnumConverter))]
        public SportsFacilityType Type { get; set; }
    }
}