using SportyWarsaw.Domain.Enums;
using System;

namespace SportyWarsaw.WebApi.Models
{
    public class MeetingModel // nie wiem co jeszcze moge miec w dto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxParticipants { get; set; }
        public decimal? Cost { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public SportType SportType { get; set; }
    }
}