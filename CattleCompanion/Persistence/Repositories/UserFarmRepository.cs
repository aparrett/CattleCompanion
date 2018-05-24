using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CattleCompanion.Persistence.Repositories
{
    public class UserFarmRepository : IUserFarmRepository
    {
        private readonly IApplicationDbContext _context;

        public UserFarmRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(UserFarm userFarm)
        {
            _context.UserFarms.Add(userFarm);
        }

        public IEnumerable<UserFarm> GetAllByFarmId(int farmId)
        {
            return _context.UserFarms.Where(uf => uf.FarmId == farmId).ToList();
        }

        public IEnumerable<Farm> GetFarms(string userId)
        {
            return _context.UserFarms.Where(uf => uf.UserId == userId).Select(uf => uf.Farm).ToList();
        }

        public UserFarm GetUserFarm(int farmId, string userId)
        {
            return _context.UserFarms.SingleOrDefault(uf => uf.FarmId == farmId && uf.UserId == userId);
        }
    }
}