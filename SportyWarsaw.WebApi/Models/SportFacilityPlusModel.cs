using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.Domain.Enums;

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
        public Position Position { get; set; } = new Position();
        public SportsFacilityType Type { get; set; }
    }
}