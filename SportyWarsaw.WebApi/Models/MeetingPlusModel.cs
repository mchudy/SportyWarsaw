using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SportyWarsaw.Domain.Enums;
using System;

namespace SportyWarsaw.WebApi.Models
{
    public class MeetingPlusModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SportType SportType { get; set; }
        public decimal? Cost { get; set; }
        public string Description { get; set; }

        public string OrganizerName { get; set; }
        public SportFacilityPlusModel SportsFacility { get; set; }
    }
}