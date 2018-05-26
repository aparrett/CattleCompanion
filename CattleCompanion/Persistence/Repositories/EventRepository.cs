using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CattleCompanion.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IApplicationDbContext _context;

        public EventRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events.ToList();
        }
    }
}