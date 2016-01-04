using System;

namespace SportyWarsaw.WebApi.Models
{
    public class FriendshipModel
    {
        public string InviterUsername { get; set; }
        public string FriendUsername { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}