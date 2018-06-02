namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMotherFatherFKs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cows", "MotherId", c => c.Int());
            AlterColumn("dbo.Cows", "FatherId", c => c.Int());
            CreateIndex("dbo.Cows", "MotherId");
            CreateIndex("dbo.Cows", "FatherId");
            AddForeignKey("dbo.Cows", "FatherId", "dbo.Cows", "Id");
            AddForeignKey("dbo.Cows", "MotherId", "dbo.Cows", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cows", "MotherId", "dbo.Cows");
            DropForeignKey("dbo.Cows", "FatherId", "dbo.Cows");
            DropIndex("dbo.Cows", new[] { "FatherId" });
            DropIndex("dbo.Cows", new[] { "MotherId" });
            AlterColumn("dbo.Cows", "FatherId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cows", "MotherId", c => c.Int(nullable: false));
        }
    }
}
