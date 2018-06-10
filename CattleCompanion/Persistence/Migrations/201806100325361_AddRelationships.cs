namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cows", "FatherId", "dbo.Cows");
            DropForeignKey("dbo.Cows", "MotherId", "dbo.Cows");
            DropIndex("dbo.Cows", new[] { "MotherId" });
            DropIndex("dbo.Cows", new[] { "FatherId" });
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cow1Id = c.Int(nullable: false),
                        Cow2Id = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cows", t => t.Cow1Id)
                .ForeignKey("dbo.Cows", t => t.Cow2Id)
                .Index(t => t.Cow1Id)
                .Index(t => t.Cow2Id);
            
            DropColumn("dbo.Cows", "MotherId");
            DropColumn("dbo.Cows", "FatherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cows", "FatherId", c => c.Int());
            AddColumn("dbo.Cows", "MotherId", c => c.Int());
            DropForeignKey("dbo.Relationships", "Cow2Id", "dbo.Cows");
            DropForeignKey("dbo.Relationships", "Cow1Id", "dbo.Cows");
            DropIndex("dbo.Relationships", new[] { "Cow2Id" });
            DropIndex("dbo.Relationships", new[] { "Cow1Id" });
            DropTable("dbo.Relationships");
            CreateIndex("dbo.Cows", "FatherId");
            CreateIndex("dbo.Cows", "MotherId");
            AddForeignKey("dbo.Cows", "MotherId", "dbo.Cows", "Id");
            AddForeignKey("dbo.Cows", "FatherId", "dbo.Cows", "Id");
        }
    }
}
