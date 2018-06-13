using CattleCompanion.Core.Models;

namespace CattleCompanion.Core.Repositories
{
    public interface IRelationshipRepository
    {
        void Add(Relationship relationship);
        Relationship GetRelationship(int cow1Id, int cow2Id);
        void Delete(Relationship relationship);
        void DeleteAll(int id);
    }
}