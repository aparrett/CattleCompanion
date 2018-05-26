using CattleCompanion.Core.Models;
using System.Data.Entity;

namespace CattleCompanion.Core
{
    public interface IApplicationDbContext
    {
        DbSet<Farm> Farms { get; set; }
        DbSet<UserFarm> UserFarms { get; set; }
        DbSet<Cow> Cattle { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<CowEvent> CowEvents { get; set; }
    }
}