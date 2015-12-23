using Newtonsoft.Json;
using SportyWarsaw.Domain.Data;
using SportyWarsaw.Domain.Enums;
using System.Collections.Generic;

namespace SportyWarsaw.Domain.Entities
{
    [JsonConverter(typeof(SportsFacilityConverter))]
    public class SportsFacility
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string District { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public Position Position { get; set; } = new Position();
        public SportsFacilityType Type { get; set; }

        public virtual IList<EmailAddress> Emails { get; set; } = new List<EmailAddress>();
    }
}
