namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActuallyAddUniqueContraintToFarmUrl : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Farms", new[] { "Url" });
            CreateIndex("dbo.Farms", "Url", unique: true, name: "UC_Url");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Farms", "UC_Url");
            CreateIndex("dbo.Farms", "Url");
        }
    }
}
