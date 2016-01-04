using SportyWarsaw.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SportyWarsaw.WebApi.Models
{
    public class AddMeetingModel
    {
        [Required]
        public string Title { get; set; }

        [Range(1, int.MaxValue)]
        public int MaxParticipants { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        //TODO check if after StartTime
        [Required]
        public DateTime EndTime { get; set; }

        public SportType SportType { get; set; }

        public decimal? Cost { get; set; }

        public string Description { get; set; }

        [Required]
        public int SportsFacilityId { get; set; }
    }
}