namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFarms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFarms",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FarmId = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FarmId })
                .ForeignKey("dbo.Farms", t => t.FarmId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FarmId);
            
            DropColumn("dbo.Farms", "IsDefault");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Farms", "IsDefault", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.UserFarms", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserFarms", "FarmId", "dbo.Farms");
            DropIndex("dbo.UserFarms", new[] { "FarmId" });
            DropIndex("dbo.UserFarms", new[] { "UserId" });
            DropTable("dbo.UserFarms");
        }
    }
}
