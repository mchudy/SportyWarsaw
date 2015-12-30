using System;
using System.Collections.Generic;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.Domain.Enums;

namespace SportyWarsaw.WebApi.Models
{
    public class MeetingPlusModel
    {
        public int Id { get; set; }
        public virtual User Organizer { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public SportType SportType { get; set; }
        public decimal? Cost { get; set; }
        public string Description { get; set; }

        public  SportsFacility SportsFacility { get; set; }
    }
}