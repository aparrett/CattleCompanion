namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdAndDateToCowEvents : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CowEvents");
            AddColumn("dbo.CowEvents", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CowEvents", "Date", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.CowEvents", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CowEvents");
            DropColumn("dbo.CowEvents", "Date");
            DropColumn("dbo.CowEvents", "Id");
            AddPrimaryKey("dbo.CowEvents", new[] { "CowId", "EventId" });
        }
    }
}
