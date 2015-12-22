using SportyWarsaw.Domain.Data;
using System.Linq;

namespace SportyWarsaw.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SportyWarsaw.Domain.SportyWarsawContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SportyWarsawContext context)
        {
            var facilities = new SportsFacilitiesDownloader().GetSportsFacilities().Result;
            foreach (var facility in facilities)
            {
                var entity = context.SportsFacilities.FirstOrDefault(f => f.Street == facility.Street && f.Number == facility.Number
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
            context.SaveChanges();
        }
    }
}
