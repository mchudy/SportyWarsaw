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
        public IDbSet<EmailAddress> EmailAddresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meeting>()
                        .HasMany(c => c.Participants).WithMany(i => i.Meetings)
                        .Map(t => t.MapLeftKey("MeetingId")
                                   .MapRightKey("UserId")
                                   .ToTable("UsersMeetings"));

            modelBuilder.Entity<User>()
                        .HasMany(u => u.FriendshipsInitiated)
                        .WithRequired(ul => ul.Inviter)
                        .HasForeignKey(ul => ul.InviterId);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.FriendshipsRequested)
                        .WithRequired(ul => ul.Friend)
                        .HasForeignKey(ul => ul.FriendId)
                        .WillCascadeOnDelete(false);
        }
    }
}
