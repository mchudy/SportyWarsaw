using System;

namespace SportyWarsaw.WebApi.Models
{
    public class FriendshipModel
    {
        public string InviterId { get; set; }
        public string FriendId { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}