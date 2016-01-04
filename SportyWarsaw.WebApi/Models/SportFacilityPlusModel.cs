using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.Domain.Enums;
using System.Collections.Generic;

namespace SportyWarsaw.WebApi.Models
{
    public class SportFacilityPlusModel
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string District { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public IList<string> Emails { get; set; } = new List<string>();
        public Position Position { get; set; } = new Position();

        [JsonConverter(typeof(StringEnumConverter))]
        public SportsFacilityType Type { get; set; }
    }
}