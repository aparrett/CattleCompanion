using CattleCompanion.Core.Models;

namespace CattleCompanion.Core.Repositories
{
    public interface IFarmRepository
    {
        Farm GetFarm(int id);
        void Add(Farm farm);
    }
}