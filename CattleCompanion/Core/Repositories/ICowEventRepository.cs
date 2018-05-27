using CattleCompanion.Core.Models;

namespace CattleCompanion.Core.Repositories
{
    public interface ICowEventRepository
    {
        void Add(CowEvent cowEvent);
    }
}