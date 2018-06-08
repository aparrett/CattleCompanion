using CattleCompanion.Core.Models;
using System.Collections.Generic;

namespace CattleCompanion.Core.Repositories
{
    public interface IUserFarmRepository
    {
        void Add(UserFarm userFarm);
        IEnumerable<UserFarm> GetAllByFarmId(int farmId);
        IEnumerable<Farm> GetFarms(string userId);
        UserFarm GetUserFarm(int farmId, string userId);
        void Remove(UserFarm userFarm);
    }
}