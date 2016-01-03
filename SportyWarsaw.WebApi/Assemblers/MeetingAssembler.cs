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
                Title = entity.Title,
                Cost = entity.Cost,
                MaxParticipants = entity.MaxParticipants,
                EndTime = entity.EndTime,
                StartTime = entity.StartTime,
                SportType = entity.SportType
            };
        }

        public MeetingPlusModel ToMeetingPlusModel(Meeting entity)
        {
            SportsFacilitiesAssembler assembler = new SportsFacilitiesAssembler();
            MeetingPlusModel model = new MeetingPlusModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                MaxParticipants = entity.MaxParticipants,
                Cost = entity.Cost,
                EndTime = entity.EndTime,
                OrganizerName = entity.Organizer.UserName,
                SportType = entity.SportType,
                StartTime = entity.StartTime
            };
            model.SportsFacility = assembler.ToSportFacilityPlusModel(entity.SportsFacility);
            return model;
        }
    }
}