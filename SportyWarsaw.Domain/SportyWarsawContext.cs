using Microsoft.AspNet.Identity.EntityFramework;
using SportyWarsaw.Domain.Entities;
using System.Data.Entity;

namespace SportyWarsaw.Domain
{
    public class SportyWarsawContext : IdentityDbContext<User>
    {
        public SportyWarsawContext()
            : base("LocalDbConnection")
        {
        }

        public IDbSet<SportsFacility> SportsFacilities { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Meeting> Meetings { get; set; }
    }
}
