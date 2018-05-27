using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.Repositories;
using System.Linq;

namespace CattleCompanion.Persistence.Repositories
{
    public class CowRepository : ICowRepository
    {
        private IApplicationDbContext _context;

        public CowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Cow cow)
        {
            _context.Cattle.Add(cow);
        }

        public Cow GetCow(int id)
        {
            return _context.Cattle.SingleOrDefault(c => c.Id == id);
        }
    }
}