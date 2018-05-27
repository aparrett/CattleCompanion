namespace CattleCompanion.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedEvents : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[Events] ([Name], [Description]) VALUES (N'Broken Bone', N'The Cow broke a bone.')
                INSERT INTO [dbo].[Events] ([Name], [Description]) VALUES (N'Heart Attack', N'The Cow had a heart attack.')
            ");
        }

        public override void Down()
        {
        }
    }
}
