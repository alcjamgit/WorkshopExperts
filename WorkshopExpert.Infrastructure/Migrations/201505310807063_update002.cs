namespace WorkshopExpert.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update002 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Developments", new[] { "Workshop_Id" });
            AlterColumn("dbo.Developments", "Workshop_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Developments", "Workshop_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Developments", new[] { "Workshop_Id" });
            AlterColumn("dbo.Developments", "Workshop_Id", c => c.Guid());
            CreateIndex("dbo.Developments", "Workshop_Id");
        }
    }
}
