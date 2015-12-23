using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportyWarsaw.Domain.Entities
{
    public class Friendship
    {
        [Key, Column(Order = 0)]
        public string InviterId { get; set; }

        [Key, Column(Order = 1)]
        public string FriendId { get; set; }

        public virtual User Inviter { get; set; }
        public virtual User Friend { get; set; }

        public bool IsConfirmed { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
