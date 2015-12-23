namespace SportyWarsaw.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixFriendship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friends", "InviterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "FriendId", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "InviterId" });
            DropIndex("dbo.Friends", new[] { "FriendId" });
            CreateTable(
                "dbo.Friendships",
                c => new
                {
                    InviterId = c.String(nullable: false, maxLength: 128),
                    FriendId = c.String(nullable: false, maxLength: 128),
                    IsConfirmed = c.Boolean(nullable: false),
                    CreatedTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.InviterId, t.FriendId })
                .ForeignKey("dbo.AspNetUsers", t => t.FriendId)
                .ForeignKey("dbo.AspNetUsers", t => t.InviterId, cascadeDelete: true)
                .Index(t => t.InviterId)
                .Index(t => t.FriendId);

            DropTable("dbo.Friends");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                {
                    InviterId = c.String(nullable: false, maxLength: 128),
                    FriendId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.InviterId, t.FriendId });

            DropForeignKey("dbo.Friendships", "InviterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friendships", "FriendId", "dbo.AspNetUsers");
            DropIndex("dbo.Friendships", new[] { "FriendId" });
            DropIndex("dbo.Friendships", new[] { "InviterId" });
            DropTable("dbo.Friendships");
            CreateIndex("dbo.Friends", "FriendId");
            CreateIndex("dbo.Friends", "InviterId");
            AddForeignKey("dbo.Friends", "FriendId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friends", "InviterId", "dbo.AspNetUsers", "Id");
        }
    }
}
