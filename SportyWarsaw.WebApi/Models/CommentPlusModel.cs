using System;
using SportyWarsaw.Domain.Entities;

namespace SportyWarsaw.WebApi.Models
{
    public class CommentPlusModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public  User User { get; set; }
    }
}