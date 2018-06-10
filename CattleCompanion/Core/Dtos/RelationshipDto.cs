using CattleCompanion.Core.Models;

namespace CattleCompanion.Core.Dtos
{
    public class RelationshipDto
    {
        public int Id { get; set; }
        public int Cow1Id { get; set; }
        public int Cow2Id { get; set; }
        public RelationshipType Type { get; set; }
    }
}