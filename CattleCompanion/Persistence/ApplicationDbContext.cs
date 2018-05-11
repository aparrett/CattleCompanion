using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CattleCompanion.Persistence
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Farm> Farms { get; set; }
        public DbSet<UserFarm> UserFarms { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FarmConfiguration());
            modelBuilder.Configurations.Add(new UserFarmConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}