using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;
namespace SportyWarsaw.WebApi.Assemblers
{
    public interface IMeetingAssembler
    {
        MeetingModel ToMeetingModel(Meeting entity);
        MeetingPlusModel ToMeetingPlusModel(Meeting entity);
        Meeting ToMeetingFromPlusModel(MeetingPlusModel model);
        Meeting ToMeetingFromModel(MeetingModel model);
    }
}
