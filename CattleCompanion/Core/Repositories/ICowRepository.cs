using CattleCompanion.Core.Models;
using System.Collections.Generic;

namespace CattleCompanion.Core.Repositories
{
    public interface ICowRepository
    {
        void Add(Cow cow);
        void Remove(Cow cow);
        Cow GetCow(int id);
        IEnumerable<Cow> GetChildren(Cow cow);
        IEnumerable<Cow> GetSiblings(Cow cow);
        IEnumerable<Cow> GetAllByFarm(int id);
    }
}