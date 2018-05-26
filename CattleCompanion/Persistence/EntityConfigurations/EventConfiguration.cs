using CattleCompanion.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CattleCompanion.Persistence.EntityConfigurations
{
    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            Property(e => e.Name)
                .IsRequired();

            Property(e => e.Description)
                .IsRequired();
        }
    }
}