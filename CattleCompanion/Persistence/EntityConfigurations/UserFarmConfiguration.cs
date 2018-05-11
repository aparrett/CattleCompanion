using CattleCompanion.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace CattleCompanion.Persistence.EntityConfigurations
{
    public class UserFarmConfiguration : EntityTypeConfiguration<UserFarm>
    {
        public UserFarmConfiguration()
        {
            HasKey(uf => new { uf.UserId, uf.FarmId });

            HasRequired(uf => uf.User)
                .WithMany(u => u.UserFarms)
                .WillCascadeOnDelete(false);
        }
    }
}