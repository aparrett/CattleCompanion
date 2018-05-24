namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCattle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GivenId = c.String(nullable: false),
                        FarmId = c.Int(nullable: false),
                        MotherId = c.Int(nullable: false),
                        FatherId = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 1),
                        IsDeceased = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Farms", t => t.FarmId, cascadeDelete: true)
                .Index(t => t.FarmId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cows", "FarmId", "dbo.Farms");
            DropIndex("dbo.Cows", new[] { "FarmId" });
            DropTable("dbo.Cows");
        }
    }
}
