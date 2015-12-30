using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportyWarsaw.Domain.Entities;

namespace SportyWarsaw.WebApi.Models
{
    public class UserPlusModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Meeting> Meetings { get; set; } 

        /// <summary>
        /// Friendships that the user initiated himself (i.e. he sent the friend request)
        /// </summary>
        public ICollection<Friendship> FriendshipsInitiated { get; set; } 

        /// <summary>
        /// Friendships that were requested from the user (i.e. he received the friend request)
        /// </summary>
        public  ICollection<Friendship> FriendshipsRequested { get; set; }

    }
}