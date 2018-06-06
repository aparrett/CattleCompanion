namespace CattleCompanion.Core.Dtos
{
    public class CowDto
    {
        public int Id { get; set; }
        public int? MotherId { get; set; }
        public int? FatherId { get; set; }
        public int? ChildId { get; set; }
        public int? ParentId { get; set; }
    }
}