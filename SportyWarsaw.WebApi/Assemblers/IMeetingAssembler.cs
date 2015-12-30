using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;
namespace SportyWarsaw.WebApi.Assemblers
{
    public interface IMeetingAssembler
    {
        MeetingModel ToMeetingModel(Meeting entity);
        MeetingPlusModel ToMeetingPlusModel(Meeting entity);
    }
}
