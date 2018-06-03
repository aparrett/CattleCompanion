using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.Repositories;
using System.Collections.Generic;
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

        public IEnumerable<Cow> GetChildren(Cow cow)
        {
            return _context.Cattle
                .Where(c => c.FatherId == cow.Id || c.MotherId == cow.Id)
                .OrderBy(c => c.GivenId)
                .ToList();
        }

        public IEnumerable<Cow> GetSiblings(Cow cow)
        {
            return _context.Cattle
                .Where(c => (c.FatherId == cow.FatherId && c.Id != cow.Id && c.FatherId != null)
                            || (c.MotherId == cow.MotherId && c.Id != cow.Id && c.MotherId != null))
                .OrderBy(c => c.GivenId)
                .ToList();
        }

        public IEnumerable<Cow> GetAllByFarm(int id)
        {
            return _context.Cattle
                .Where(c => c.FarmId == id)
                .OrderBy(c => c.GivenId)
                .ToList();
        }

        public void Remove(Cow cow)
        {
            _context.Cattle.Remove(cow);
        }
    }
}