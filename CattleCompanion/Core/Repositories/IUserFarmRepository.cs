using CattleCompanion.Core.Models;
using System.Collections.Generic;

namespace CattleCompanion.Core.Repositories
{
    public interface IUserFarmRepository
    {
        void Add(UserFarm userFarm);
        IEnumerable<UserFarm> GetAllByFarmId(int farmId);
        UserFarm GetUserFarm(int farmId, string userId);
    }
}