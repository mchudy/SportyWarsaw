using SportyWarsaw.Domain.Data;

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
            var facilities = downloader.GetSportsFacilities().Result;
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
