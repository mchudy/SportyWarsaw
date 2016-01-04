namespace SportyWarsaw.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Text = c.String(),
                    Date = c.DateTime(nullable: false),
                    Meeting_Id = c.Int(),
                    User_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meetings", t => t.Meeting_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Meeting_Id)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    FirstName = c.String(),
                    LastName = c.String(),
                    Picture = c.Binary(),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

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
                .ForeignKey("dbo.AspNetUsers", t => t.InviterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.FriendId)
                .Index(t => t.InviterId)
                .Index(t => t.FriendId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Meetings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    MaxParticipants = c.Int(nullable: false),
                    StartTime = c.DateTime(nullable: false),
                    EndTime = c.DateTime(nullable: false),
                    SportType = c.Int(nullable: false),
                    Cost = c.Decimal(precision: 18, scale: 2),
                    Description = c.String(),
                    Organizer_Id = c.String(maxLength: 128),
                    SportsFacility_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Organizer_Id)
                .ForeignKey("dbo.SportsFacilities", t => t.SportsFacility_Id, cascadeDelete: true)
                .Index(t => t.Organizer_Id)
                .Index(t => t.SportsFacility_Id);

            CreateTable(
                "dbo.SportsFacilities",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Street = c.String(),
                    Number = c.String(),
                    Description = c.String(),
                    District = c.String(),
                    PhoneNumber = c.String(),
                    Website = c.String(),
                    Position_Latitude = c.Double(nullable: false),
                    Position_Longitude = c.Double(nullable: false),
                    Type = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.EmailAddresses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Email = c.String(),
                    SportsFacility_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SportsFacilities", t => t.SportsFacility_Id, cascadeDelete: true)
                .Index(t => t.SportsFacility_Id);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.UsersMeetings",
                c => new
                {
                    MeetingId = c.Int(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.MeetingId, t.UserId })
                .ForeignKey("dbo.Meetings", t => t.MeetingId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MeetingId)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Meetings", "SportsFacility_Id", "dbo.SportsFacilities");
            DropForeignKey("dbo.EmailAddresses", "SportsFacility_Id", "dbo.SportsFacilities");
            DropForeignKey("dbo.UsersMeetings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersMeetings", "MeetingId", "dbo.Meetings");
            DropForeignKey("dbo.Meetings", "Organizer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Meeting_Id", "dbo.Meetings");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friendships", "FriendId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friendships", "InviterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UsersMeetings", new[] { "UserId" });
            DropIndex("dbo.UsersMeetings", new[] { "MeetingId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.EmailAddresses", new[] { "SportsFacility_Id" });
            DropIndex("dbo.Meetings", new[] { "SportsFacility_Id" });
            DropIndex("dbo.Meetings", new[] { "Organizer_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Friendships", new[] { "FriendId" });
            DropIndex("dbo.Friendships", new[] { "InviterId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "Meeting_Id" });
            DropTable("dbo.UsersMeetings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.EmailAddresses");
            DropTable("dbo.SportsFacilities");
            DropTable("dbo.Meetings");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Friendships");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
        }
    }
}
