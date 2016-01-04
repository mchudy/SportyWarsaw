using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;

namespace SportyWarsaw.WebApi.Assemblers
{
    public class CommentAssembler : ICommentAssembler
    {
        public CommentModel ToCommentModel(Comment entity)
        {
            return new CommentModel
            {
                Id = entity.Id,
                Date = entity.Date,
                Text = entity.Text,
                Username = entity.User.UserName,
                MeetingId = entity.Meeting.Id
            };
        }

    }
}