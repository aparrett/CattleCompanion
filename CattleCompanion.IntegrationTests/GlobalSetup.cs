﻿using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;
using CattleCompanion.Core.Models;

namespace CattleCompanion.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new CattleCompanion.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser
            {
                UserName = "user1",
                Email = "-",
                PasswordHash = "-"
            });

            context.Users.Add(new ApplicationUser
            {
                UserName = "user2",
                Email = "-",
                PasswordHash = "-"
            });

            context.SaveChanges();
        }
    }
}
