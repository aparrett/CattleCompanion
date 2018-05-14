namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FarmUrlIsUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Farms", "Url");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Farms", new[] { "Url" });
        }
    }
}
