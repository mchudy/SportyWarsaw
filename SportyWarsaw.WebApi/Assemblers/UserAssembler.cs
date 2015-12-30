using System.Data.Entity.Core.Metadata.Edm;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;

namespace SportyWarsaw.WebApi.Assemblers
{
    public class UserAssembler
    {
        public UserModel ToUserModel(User entity)
        {
            return new UserModel()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }

        public UserPlusModel ToUserPlusModel(User entity)
        {
            return new UserPlusModel()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Meetings = entity.Meetings,
                FriendshipsInitiated = entity.FriendshipsInitiated,
                FriendshipsRequested = entity.FriendshipsRequested
            };
        }

    }
}