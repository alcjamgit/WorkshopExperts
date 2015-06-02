namespace WorkshopExpert.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update003 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeSlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Developments", "TimeSlot_Id");
            AddForeignKey("dbo.Developments", "TimeSlot_Id", "dbo.TimeSlots", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Developments", "TimeSlot_Id", "dbo.TimeSlots");
            DropIndex("dbo.Developments", new[] { "TimeSlot_Id" });
            DropTable("dbo.TimeSlots");
        }
    }
}
