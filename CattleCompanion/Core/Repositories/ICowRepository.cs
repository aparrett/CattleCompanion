using CattleCompanion.Core.Models;
using System.Collections.Generic;

namespace CattleCompanion.Core.Repositories
{
    public interface ICowRepository
    {
        void Add(Cow cow);
        void Remove(Cow cow);
        Cow GetCow(int id);
        Cow GetCowWithEvents(int id);
        IEnumerable<Cow> GetSiblings(Cow cow);
        IEnumerable<Cow> GetAllByFarm(int id);
        IEnumerable<Cow> GetAllByUserId(string userId);
    }
}