using CattleCompanion.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CattleCompanion.Persistence.EntityConfigurations
{
    public class FarmConfiguration : EntityTypeConfiguration<Farm>
    {
        public FarmConfiguration()
        {
            Property(f => f.Name)
                .IsRequired();

            Property(f => f.Url)
                .IsRequired();
        }
    }
}