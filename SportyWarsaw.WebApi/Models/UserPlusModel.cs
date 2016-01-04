using System.Collections.Generic;

namespace SportyWarsaw.WebApi.Models
{
    public class UserPlusModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
        public string Username { get; set; }

        public IList<MeetingPlusModel> Meetings { get; set; }

        public IList<FriendshipModel> FriendshipsInitiated { get; set; }

        public IList<FriendshipModel> FriendshipsRequested { get; set; }

    }
}