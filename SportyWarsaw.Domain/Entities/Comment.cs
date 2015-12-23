using System;

namespace SportyWarsaw.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
    }
}