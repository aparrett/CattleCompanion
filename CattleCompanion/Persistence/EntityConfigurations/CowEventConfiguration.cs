using CattleCompanion.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CattleCompanion.Persistence.EntityConfigurations
{
    public class CowEventConfiguration : EntityTypeConfiguration<CowEvent>
    {
        public CowEventConfiguration()
        {
            HasKey(ce => new { ce.CowId, ce.EventId });

            HasRequired(ce => ce.Cow)
                .WithMany(c => c.CowEvents)
                .WillCascadeOnDelete(false);
        }
    }
}