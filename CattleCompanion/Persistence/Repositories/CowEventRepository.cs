using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.Repositories;

namespace CattleCompanion.Persistence.Repositories
{
    public class CowEventRepository : ICowEventRepository
    {
        private readonly IApplicationDbContext _context;

        public CowEventRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(CowEvent cowEvent)
        {
            _context.CowEvents.Add(cowEvent);
        }
    }
}