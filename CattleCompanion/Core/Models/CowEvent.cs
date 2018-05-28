using System;

namespace CattleCompanion.Core.Models
{
    public class CowEvent
    {
        public int Id { get; set; }
        public int CowId { get; set; }
        public int EventId { get; set; }
        public string Description { get; set; }
        public virtual Cow Cow { get; set; }
        public virtual Event Event { get; set; }
        public DateTime Date { get; set; }
    }
}