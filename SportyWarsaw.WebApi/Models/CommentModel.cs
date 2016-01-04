using System;

namespace SportyWarsaw.WebApi.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public int MeetingId { get; set; }
    }
}