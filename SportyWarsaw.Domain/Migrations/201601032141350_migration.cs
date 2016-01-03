namespace SportyWarsaw.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meetings", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meetings", "Title");
        }
    }
}
