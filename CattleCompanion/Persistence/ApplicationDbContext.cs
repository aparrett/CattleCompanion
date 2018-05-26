using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CattleCompanion.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Farm> Farms { get; set; }
        public DbSet<UserFarm> UserFarms { get; set; }
        public DbSet<Cow> Cattle { get; set; }
        public DbSet<Event> Events { get; set; }

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
            modelBuilder.Configurations.Add(new CowConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}