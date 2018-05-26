namespace CattleCompanion.Core.Models
{
    public class CowEvent
    {
        public int CowId { get; set; }
        public int EventId { get; set; }
        public virtual Cow Cow { get; set; }
        public virtual Event Event { get; set; }
    }
}