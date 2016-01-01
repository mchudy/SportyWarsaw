using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SportyWarsaw.Domain.Data;
using SportyWarsaw.Domain.Entities;
using SportyWarsaw.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;

namespace SportyWarsaw.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SportyWarsaw.Domain.SportyWarsawContext>
    {
        private readonly ISportsFacilitiesDownloader downloader = new SportsFacilitiesDownloader();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SportyWarsawContext context)
        {
            try
            {
                AddSportsFacilities(context);
                AddUsers(context);
                AddFriends(context);
                AddMeetings(context);
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure?.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}\n", error.PropertyName, error.ErrorMessage);
                    }
                }
                throw new DbEntityValidationException("Could not add entity to the database:\n" + sb, ex);
            }
        }

        private void AddMeetings(SportyWarsawContext context)
        {
            var user1 = context.Users.FirstOrDefault(e => e.UserName == "user1");
            var user2 = context.Users.FirstOrDefault(e => e.UserName == "user2");
            var user3 = context.Users.FirstOrDefault(e => e.UserName == "user3");
            var meeting1 = new Meeting
            {
                Id = 1,
                Cost = 10m,
                Description = "Gra w pi³kê",
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1).AddHours(2),
                MaxParticipants = 22,
                Organizer = user1,
                SportType = SportType.Football,
                SportsFacility = context.SportsFacilities.Find(1),
            };
            meeting1.Participants.Add(user1);
            meeting1.Participants.Add(user2);
            meeting1.Participants.Add(user3);
            meeting1.Comments.Add(new Comment
            {
                Date = DateTime.Now,
                Text = "Bêdê",
                User = user2
            });
            meeting1.Comments.Add(new Comment
            {
                Date = DateTime.Now,
                Text = "Ja te¿",
                User = user3
            });
            context.Meetings.AddOrUpdate(meeting1);
            var meeting2 = new Meeting
            {
                Id = 2,
                Cost = 10m,
                Description = "Gra w pi³kê",
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1).AddHours(2),
                MaxParticipants = 22,
                Organizer = user2,
                SportType = SportType.Football,
                SportsFacility = context.SportsFacilities.Find(2),
            };
            meeting2.Participants.Add(user2);
            meeting2.Participants.Add(user1);
            meeting2.Participants.Add(user3);
            context.Meetings.AddOrUpdate(meeting2);
        }

        private void AddFriends(SportyWarsawContext context)
        {
            var user1 = context.Users.FirstOrDefault(e => e.UserName == "user1");
            var user2 = context.Users.FirstOrDefault(e => e.UserName == "user2");
            var user3 = context.Users.FirstOrDefault(e => e.UserName == "user3");
            if (!user1.FriendshipsInitiated.Any(f => f.FriendId == user2.Id))
            {
                user1.FriendshipsInitiated.Add(new Friendship
                {
                    Inviter = user1,
                    Friend = user2,
                    CreatedTime = DateTime.Now,
                    IsConfirmed = true
                });
            }
            if (!user1.FriendshipsInitiated.Any(f => f.FriendId == user3.Id))
            {
                user1.FriendshipsInitiated.Add(new Friendship
                {
                    Inviter = user1,
                    Friend = user3,
                    CreatedTime = DateTime.Now,
                    IsConfirmed = true
                });
            }
            if (!user2.FriendshipsInitiated.Any(f => f.FriendId == user3.Id))
            {
                user2.FriendshipsInitiated.Add(new Friendship
                {
                    Inviter = user2,
                    Friend = user3,
                    CreatedTime = DateTime.Now,
                    IsConfirmed = false
                });
            }
        }

        private void AddUsers(SportyWarsawContext context)
        {
            string testPassword = "test123";
            var users = new[]
            {
                new {UserName = "user1", Email = "user1@sportywarsaw.pl"},
                new {UserName = "user2", Email = "user2@sportywarsaw.pl"},
                new {UserName = "user3", Email = "user3@sportywarsaw.pl"},
                new {UserName = "user4", Email = "user4@sportywarsaw.pl"},
                new {UserName = "user5", Email = "user5@sportywarsaw.pl"},
            };
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            foreach (var user in users)
            {
                if (!context.Users.Any(u => user.UserName == u.UserName))
                {
                    var newUser = new User { UserName = user.UserName, Email = user.Email };
                    var result = userManager.Create(newUser, testPassword);
                    if (!result.Succeeded)
                    {
                        var results =
                            result.Errors.Select(
                                e =>
                                    new DbEntityValidationResult(context.Entry(newUser),
                                        new[] { new DbValidationError("", e) }));
                        throw new DbEntityValidationException("", new List<DbEntityValidationResult>(results));
                    }
                }
            }
        }

        private void AddSportsFacilities(SportyWarsawContext context)
        {
            var facilities = downloader.GetSportsFacilities().Result;
            foreach (var facility in facilities)
            {
                var entity =
                    context.SportsFacilities.FirstOrDefault(f => f.Street == facility.Street && f.Number == facility.Number
                                                                 && f.Description == facility.Description);
                if (entity != null)
                {
                    facility.Id = entity.Id;
                    foreach (var email in entity.Emails)
                    {
                        var newEmail = facility.Emails.FirstOrDefault(e => e.Email == email.Email);
                        if (newEmail != null)
                        {
                            newEmail.Id = email.Id;
                        }
                        else
                        {
                            context.EmailAddresses.Remove(email);
                        }
                    }
                }
                context.SportsFacilities.AddOrUpdate(facility);
            }
        }
    }
}
