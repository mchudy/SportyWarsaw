namespace SportyWarsaw.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAdministrativeUnit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SportsFacilities", "AdministrativeUnit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SportsFacilities", "AdministrativeUnit", c => c.String());
        }
    }
}
