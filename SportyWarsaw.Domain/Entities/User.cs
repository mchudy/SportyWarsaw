using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportyWarsaw.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; } = new HashSet<Meeting>();

        /// <summary>
        /// Friendships that the user initiated himself (i.e. he sent the friend request)
        /// </summary>
        public virtual ICollection<Friendship> FriendshipsInitiated { get; set; } = new HashSet<Friendship>();

        /// <summary>
        /// Friendships that were requested from the user (i.e. he received the friend request)
        /// </summary>
        public virtual ICollection<Friendship> FriendshipsRequested { get; set; } = new HashSet<Friendship>();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager,
            string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
