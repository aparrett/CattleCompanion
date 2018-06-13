using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace CattleCompanion.Persistence.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private IApplicationDbContext _context;

        public RelationshipRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Relationship relationship)
        {
            _context.Relationships.Add(relationship);
        }

        public Relationship GetRelationship(int cow1Id, int cow2Id)
        {
            return _context.Relationships
                    .Include(r => r.Cow1)
                    .Include(r => r.Cow2)
                    .SingleOrDefault(r => r.Cow1Id == cow1Id && r.Cow2Id == cow2Id);
        }

        public void Delete(Relationship relationship)
        {
            _context.Relationships.Remove(relationship);
        }

        public void DeleteAll(int id)
        {
            var relationships = _context.Relationships.Where(r => r.Cow1Id == id || r.Cow2Id == id).ToList();
            relationships.ForEach(r => _context.Relationships.Remove(r));
        }
    }
}