namespace SportyWarsaw.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendshipImprovements : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friendships", "InviterId", "dbo.AspNetUsers");
            DropIndex("dbo.Friendships", new[] { "InviterId" });
            DropColumn("dbo.Friendships", "FriendId");
            RenameColumn(table: "dbo.Friendships", name: "InviterId", newName: "FriendId");
            CreateIndex("dbo.Friendships", "InviterId");
            AddForeignKey("dbo.Friendships", "FriendId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friendships", "FriendId", "dbo.AspNetUsers");
            DropIndex("dbo.Friendships", new[] { "InviterId" });
            RenameColumn(table: "dbo.Friendships", name: "FriendId", newName: "InviterId");
            AddColumn("dbo.Friendships", "FriendId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Friendships", "InviterId");
            AddForeignKey("dbo.Friendships", "InviterId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
