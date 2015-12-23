namespace SportyWarsaw.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFriendship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "User_Id" });
            RenameColumn(table: "dbo.UsersMeetings", name: "Id", newName: "MeetingId");
            RenameIndex(table: "dbo.UsersMeetings", name: "IX_Id", newName: "IX_MeetingId");
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        InviterId = c.String(nullable: false, maxLength: 128),
                        FriendId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.InviterId, t.FriendId })
                .ForeignKey("dbo.AspNetUsers", t => t.InviterId)
                .ForeignKey("dbo.AspNetUsers", t => t.FriendId)
                .Index(t => t.InviterId)
                .Index(t => t.FriendId);
            
            DropColumn("dbo.AspNetUsers", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "User_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Friends", "FriendId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "InviterId", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "FriendId" });
            DropIndex("dbo.Friends", new[] { "InviterId" });
            DropTable("dbo.Friends");
            RenameIndex(table: "dbo.UsersMeetings", name: "IX_MeetingId", newName: "IX_Id");
            RenameColumn(table: "dbo.UsersMeetings", name: "MeetingId", newName: "Id");
            CreateIndex("dbo.AspNetUsers", "User_Id");
            AddForeignKey("dbo.AspNetUsers", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
