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
            FriendshipAssembler friendship = new FriendshipAssembler();
            MeetingAssembler meeting = new MeetingAssembler();

            UserPlusModel userPlusModel = new UserPlusModel()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };

            foreach (var item in entity.Meetings)
            {
                userPlusModel.Meetings.Add(meeting.ToMeetingPlusModel(item));
            }

            foreach (var item in entity.FriendshipsInitiated)
            {
                userPlusModel.FriendshipsInitiated.Add(friendship.ToFriendshipModel(item));
            }

            foreach (var item in entity.FriendshipsRequested)
            {
                userPlusModel.FriendshipsRequested.Add(friendship.ToFriendshipModel(item));
            }
            //return new UserPlusModel()
            //{
            //    FirstName = entity.FirstName,
            //    LastName = entity.LastName,
            //    Meetings = entity.Meetings,
            //    FriendshipsInitiated = entity.FriendshipsInitiated,
            //    FriendshipsRequested = entity.FriendshipsRequested
            //};

            return userPlusModel;
        }

    }
}