using CattleCompanion.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
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
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("VARCHAR")
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
        }
    }
}