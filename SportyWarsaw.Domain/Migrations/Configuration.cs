using SportyWarsaw.Domain.Data;

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
                context.SportsFacilities.AddOrUpdate(facility);
            }
            context.SaveChanges();
        }
    }
}
