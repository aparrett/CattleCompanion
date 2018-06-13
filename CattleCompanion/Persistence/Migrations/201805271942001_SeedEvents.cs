namespace CattleCompanion.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedEvents : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Assisted Delivery')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Twins')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Pinkeye')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Lame: Join/Structural')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Lame: Foreign Object')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Lame: Fescue Foot')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Lost Calf: < 1 month old')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Lost Calf: > 1 month old')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Bloated')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Prolapse: Vaginal')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Prolapse: Uterine')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Prolapse: Rectal')
                INSERT INTO [dbo].[Events] ([Name]) VALUES (N'Abscess')
            ");
        }

        public override void Down()
        {
        }
    }
}
