namespace SportyWarsaw.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixEmailAddresses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        SportsFacility_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SportsFacilities", t => t.SportsFacility_Id)
                .Index(t => t.SportsFacility_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailAddresses", "SportsFacility_Id", "dbo.SportsFacilities");
            DropIndex("dbo.EmailAddresses", new[] { "SportsFacility_Id" });
            DropTable("dbo.EmailAddresses");
        }
    }
}
