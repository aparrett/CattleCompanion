namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FarmUrlAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Farms", "Url", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Farms", "Url", c => c.String(nullable: false));
        }
    }
}
