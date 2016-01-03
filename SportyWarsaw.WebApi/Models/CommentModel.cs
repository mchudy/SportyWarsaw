using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportyWarsaw.WebApi.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
    }
}