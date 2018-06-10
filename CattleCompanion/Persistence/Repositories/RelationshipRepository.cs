using CattleCompanion.Core;
using CattleCompanion.Core.Repositories;

namespace CattleCompanion.Persistence.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private IApplicationDbContext _context;

        public RelationshipRepository(IApplicationDbContext context)
        {
            _context = context;
        }
    }
}