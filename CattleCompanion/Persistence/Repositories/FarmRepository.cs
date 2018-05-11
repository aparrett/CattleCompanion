using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.Repositories;
using System.Linq;

namespace CattleCompanion.Persistence.Repositories
{
    public class FarmRepository : IFarmRepository
    {
        private readonly IApplicationDbContext _context;

        public FarmRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Farm GetFarm(int id)
        {
            return _context.Farms.SingleOrDefault(f => f.Id == id);
        }

        public void Add(Farm farm)
        {
            _context.Farms.Add(farm);
        }
    }
}