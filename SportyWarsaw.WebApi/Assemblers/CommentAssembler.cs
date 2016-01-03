using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;

namespace SportyWarsaw.WebApi.Assemblers
{
    public class CommentAssembler : ICommentAssembler
    {
        public CommentModel ToCommentModel(Comment entity)
        {
            return new CommentModel()
            {
                Id = entity.Id,
                Date = entity.Date,
                Text = entity.Text,
                UserId = entity.User.Id
            };
        }

    }
}