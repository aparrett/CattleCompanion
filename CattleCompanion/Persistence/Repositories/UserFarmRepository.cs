using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.Repositories;

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
    }
}