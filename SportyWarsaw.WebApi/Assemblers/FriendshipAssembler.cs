using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                FriendId = entity.FriendId,
                InviterId = entity.InviterId,
                CreatedTime = entity.CreatedTime
            };

        }
    }
}