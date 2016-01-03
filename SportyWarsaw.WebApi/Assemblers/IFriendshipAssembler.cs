using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;

namespace SportyWarsaw.WebApi.Assemblers
{
    public interface IFriendshipAssembler
    {
        FriendshipModel ToFriendshipModel(Friendship entity);
    }
}
