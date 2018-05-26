using CattleCompanion.Core.Models;

namespace CattleCompanion.Core.Repositories
{
    public interface ICowRepository
    {
        void Add(Cow cow);
        Cow GetCow(int id);
    }
}