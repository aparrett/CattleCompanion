namespace CattleCompanion.Core.Models
{
    public class Relationship
    {
        public int Id { get; set; }

        public int Cow1Id { get; set; }
        public Cow Cow1 { get; set; }

        public int Cow2Id { get; set; }
        public Cow Cow2 { get; set; }

        public RelationshipType Type { get; set; }
    }
}