namespace WorkshopExpert.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Analyses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Workshop_Id = c.Guid(nullable: false),
                        ProposedWorkshopTitle = c.String(),
                        DurationHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AttendeeProfile = c.String(),
                        ProblemToOvercome = c.String(),
                        NeedToFulfill = c.String(),
                        Workshop_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workshops", t => t.Workshop_Id1)
                .Index(t => t.Workshop_Id1);
            
            CreateTable(
                "dbo.Workshops",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Category_Id = c.Guid(nullable: false),
                        Type_Id = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(maxLength: 128),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy)
                .ForeignKey("dbo.WorkshopTypes", t => t.Type_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Type_Id)
                .Index(t => t.CreatedBy)
                .Index(t => t.ModifiedBy);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Developments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeSlot_Id = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        TopicTitle = c.String(),
                        DeliveyMethod_Id = c.Int(nullable: false),
                        Duration = c.Long(nullable: false),
                        Workshop_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeliveryMethods", t => t.DeliveyMethod_Id)
                .ForeignKey("dbo.Workshops", t => t.Workshop_Id)
                .Index(t => t.DeliveyMethod_Id)
                .Index(t => t.Workshop_Id);
            
            CreateTable(
                "dbo.DeliveryMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SummaryChecklists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        Workshop_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workshops", t => t.Workshop_Id)
                .Index(t => t.Workshop_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BusinessName = c.String(),
                        BusinessType_Id = c.Int(nullable: false),
                        Country_Id = c.Int(nullable: false),
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
                .ForeignKey("dbo.BusinessTypes", t => t.BusinessType_Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.BusinessType_Id)
                .Index(t => t.Country_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.BusinessTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.WorkshopTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Analyses", "Workshop_Id1", "dbo.Workshops");
            DropForeignKey("dbo.Workshops", "Type_Id", "dbo.WorkshopTypes");
            DropForeignKey("dbo.Workshops", "ModifiedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Workshops", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "BusinessType_Id", "dbo.BusinessTypes");
            DropForeignKey("dbo.SummaryChecklists", "Workshop_Id", "dbo.Workshops");
            DropForeignKey("dbo.Developments", "Workshop_Id", "dbo.Workshops");
            DropForeignKey("dbo.Developments", "DeliveyMethod_Id", "dbo.DeliveryMethods");
            DropForeignKey("dbo.Workshops", "Category_Id", "dbo.Categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "BusinessType_Id" });
            DropIndex("dbo.SummaryChecklists", new[] { "Workshop_Id" });
            DropIndex("dbo.Developments", new[] { "Workshop_Id" });
            DropIndex("dbo.Developments", new[] { "DeliveyMethod_Id" });
            DropIndex("dbo.Workshops", new[] { "ModifiedBy" });
            DropIndex("dbo.Workshops", new[] { "CreatedBy" });
            DropIndex("dbo.Workshops", new[] { "Type_Id" });
            DropIndex("dbo.Workshops", new[] { "Category_Id" });
            DropIndex("dbo.Analyses", new[] { "Workshop_Id1" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.WorkshopTypes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Countries");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.BusinessTypes");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SummaryChecklists");
            DropTable("dbo.DeliveryMethods");
            DropTable("dbo.Developments");
            DropTable("dbo.Categories");
            DropTable("dbo.Workshops");
            DropTable("dbo.Analyses");
        }
    }
}
