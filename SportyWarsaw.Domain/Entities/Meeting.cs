using SportyWarsaw.Domain.Enums;
using System;
using System.Collections.Generic;

namespace SportyWarsaw.Domain.Entities
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public SportType SportType { get; set; }
        public decimal? Cost { get; set; }
        public string Description { get; set; }

        public virtual User Organizer { get; set; }
        public virtual SportsFacility SportsFacility { get; set; }

        public virtual ICollection<User> Participants { get; set; } = new HashSet<User>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}