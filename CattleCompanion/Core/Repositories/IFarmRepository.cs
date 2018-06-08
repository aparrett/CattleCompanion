using CattleCompanion.Core.Models;

namespace CattleCompanion.Core.Repositories
{
    public interface IFarmRepository
    {
        Farm GetFarm(int id);
        Farm GetByUrl(string url);
        void Add(Farm farm);
        void Remove(Farm farm);
    }
}