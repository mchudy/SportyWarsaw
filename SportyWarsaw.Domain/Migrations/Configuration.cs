namespace SportyWarsaw.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SportyWarsaw.Domain.SportyWarsawContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SportyWarsaw.Domain.SportyWarsawContext context)
        {

        }
    }
}
