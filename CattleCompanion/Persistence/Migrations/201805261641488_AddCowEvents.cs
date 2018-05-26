namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCowEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CowEvents",
                c => new
                    {
                        CowId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CowId, t.EventId })
                .ForeignKey("dbo.Cows", t => t.CowId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.CowId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CowEvents", "EventId", "dbo.Events");
            DropForeignKey("dbo.CowEvents", "CowId", "dbo.Cows");
            DropIndex("dbo.CowEvents", new[] { "EventId" });
            DropIndex("dbo.CowEvents", new[] { "CowId" });
            DropTable("dbo.CowEvents");
        }
    }
}
