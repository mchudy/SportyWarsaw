namespace SportyWarsaw.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendSportsFacility : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SportsFacilities", "Position_Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.SportsFacilities", "Position_Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.SportsFacilities", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SportsFacilities", "Type");
            DropColumn("dbo.SportsFacilities", "Position_Longitude");
            DropColumn("dbo.SportsFacilities", "Position_Latitude");
        }
    }
}
