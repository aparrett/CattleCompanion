namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDeleteCowEvents : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CowEvents", "CowId", "dbo.Cows");
            AddForeignKey("dbo.CowEvents", "CowId", "dbo.Cows", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CowEvents", "CowId", "dbo.Cows");
            AddForeignKey("dbo.CowEvents", "CowId", "dbo.Cows", "Id");
        }
    }
}
