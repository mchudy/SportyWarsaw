using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.WebApi.Models;

namespace SportyWarsaw.WebApi.Assemblers
{
    public class CommentAssembler
    {
        public CommentModel ToCommentModel(Comment entity)
        {
            return new CommentModel()
            {
                Id = entity.Id,
                Date = entity.Date,
                Text = entity.Text
            };
        }

        public CommentPlusModel ToCommentPlusModel(Comment entity)
        {
            return new CommentPlusModel()
            {
                Date = entity.Date,
                Id = entity.Id,
                Text = entity.Text,
                User = entity.User
            };
        }
    }
}