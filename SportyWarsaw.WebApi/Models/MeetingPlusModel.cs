using SportyWarsaw.Domain.Entities;
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
        public SportType SportType { get; set; }
        public decimal? Cost { get; set; }
        public string Description { get; set; }

        public string OrganizerName { get; set; }
        public SportsFacility SportsFacility { get; set; }
    }
}