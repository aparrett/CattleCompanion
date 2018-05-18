namespace CattleCompanion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveDefaultFarmToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DefaultFarmId", c => c.Int(nullable: false));
            DropColumn("dbo.UserFarms", "IsDefault");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserFarms", "IsDefault", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "DefaultFarmId");
        }
    }
}
