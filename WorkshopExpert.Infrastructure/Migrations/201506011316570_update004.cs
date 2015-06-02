namespace WorkshopExpert.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update004 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Analyses", new[] { "Workshop_Id1" });
            DropColumn("dbo.Analyses", "Workshop_Id");
            RenameColumn(table: "dbo.Analyses", name: "Workshop_Id1", newName: "Workshop_Id");
            AlterColumn("dbo.Analyses", "Workshop_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Analyses", "Workshop_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Analyses", new[] { "Workshop_Id" });
            AlterColumn("dbo.Analyses", "Workshop_Id", c => c.Guid());
            RenameColumn(table: "dbo.Analyses", name: "Workshop_Id", newName: "Workshop_Id1");
            AddColumn("dbo.Analyses", "Workshop_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Analyses", "Workshop_Id1");
        }
    }
}
