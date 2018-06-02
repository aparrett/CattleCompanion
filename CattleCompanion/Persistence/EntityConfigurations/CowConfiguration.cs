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

            HasOptional(c => c.Father)
                .WithMany()
                .HasForeignKey(c => c.FatherId);

            HasOptional(c => c.Mother)
                .WithMany()
                .HasForeignKey(c => c.MotherId);
        }
    }
}