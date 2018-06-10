using CattleCompanion.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CattleCompanion.Persistence.EntityConfigurations
{
    public class CowConfiguration : EntityTypeConfiguration<Cow>
    {
        public CowConfiguration()
        {
            Property(c => c.GivenId)
                .IsRequired();

            Property(c => c.FarmId)
                .IsRequired();

            Property(c => c.Gender)
                .IsRequired()
                .HasMaxLength(1);

            HasMany(c => c.Parents)
                .WithRequired(c => c.Cow2)
                .WillCascadeOnDelete(false);

            HasMany(c => c.Children)
                .WithRequired(c => c.Cow1)
                .WillCascadeOnDelete(false);
        }
    }
}