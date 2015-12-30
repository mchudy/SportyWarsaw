using System.Data.Entity.Core.Metadata.Edm;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;

namespace SportyWarsaw.WebApi.Assemblers
{
    public class MeetingAssembler : IMeetingAssembler
    {
        public MeetingModel ToMeetingModel(Meeting entity)
        {
            return new MeetingModel()
            {
                Id = entity.Id,
                Description = entity.Description,
                Cost = entity.Cost,
                MaxParticipants = entity.MaxParticipants,
                EndTime = entity.EndTime,
                StartTime = entity.StartTime,
                SportType = entity.SportType
            };
        }

        public MeetingPlusModel ToMeetingPlusModel(Meeting entity)
        {
            return new MeetingPlusModel()
            {
                Id = entity.Id,
                SportsFacility = entity.SportsFacility,
                Description = entity.Description,
                MaxParticipants = entity.MaxParticipants,
                Cost = entity.Cost,
                EndTime = entity.EndTime,
                Organizer = entity.Organizer,
                SportType = entity.SportType,
                StartTime = entity.StartTime
            };
        }
    }
}