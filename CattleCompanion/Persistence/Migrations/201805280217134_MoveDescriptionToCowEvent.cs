namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveDescriptionToCowEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CowEvents", "Description", c => c.String());
            DropColumn("dbo.Events", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Description", c => c.String(nullable: false));
            DropColumn("dbo.CowEvents", "Description");
        }
    }
}
