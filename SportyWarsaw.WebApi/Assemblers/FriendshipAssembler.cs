using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;

namespace SportyWarsaw.WebApi.Assemblers
{
    public class FriendshipAssembler : IFriendshipAssembler
    {
        public FriendshipModel ToFriendshipModel(Friendship entity)
        {
            return new FriendshipModel()
            {
                IsConfirmed = entity.IsConfirmed,
                FriendUsername = entity.Friend.UserName,
                InviterUsername = entity.Inviter.UserName,
                CreatedTime = entity.CreatedTime
            };

        }
    }
}