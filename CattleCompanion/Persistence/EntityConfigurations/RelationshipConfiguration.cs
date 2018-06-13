using CattleCompanion.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CattleCompanion.Persistence.EntityConfigurations
{
    public class RelationshipConfiguration : EntityTypeConfiguration<Relationship>
    {
        public RelationshipConfiguration()
        {
            HasRequired(r => r.Cow1)
                .WithMany(r => r.ChildrenRelationships)
                .HasForeignKey(r => r.Cow1Id)
                .WillCascadeOnDelete(true);

            HasRequired(r => r.Cow2)
                .WithMany(r => r.ParentRelationships)
                .HasForeignKey(r => r.Cow2Id)
                .WillCascadeOnDelete(true);

        }
    }
}