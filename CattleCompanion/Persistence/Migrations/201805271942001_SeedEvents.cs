namespace CattleCompanion.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedEvents : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Broken Bone')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Heart Attack')
            ");
        }

        public override void Down()
        {
        }
    }
}
