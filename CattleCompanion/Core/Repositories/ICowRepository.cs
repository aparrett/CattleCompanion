using CattleCompanion.Core.Models;

namespace CattleCompanion.Core.Repositories
{
    public interface ICowRepository
    {
        void Add(Cow cow);
        void Remove(Cow cow);
        Cow GetCow(int id);
    }
}