using System;

namespace CattleCompanion.Core.Models
{
    public class CowEvent
    {
        public int Id { get; set; }

        public int CowId { get; set; }
        public Cow Cow { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}