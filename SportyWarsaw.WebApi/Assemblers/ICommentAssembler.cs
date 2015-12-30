using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;

namespace SportyWarsaw.WebApi.Assemblers
{
    public interface ICommentAssembler
    {
        CommentModel ToCommentModel(Comment entity);
        CommentPlusModel ToCommentPlusModel(Comment entity);
    }
}
