using System;

namespace CattleCompanion.Core.Dtos
{
    public class CowEventDto
    {
        public int CowId { get; set; }
        public int EventId { get; set; }
        public DateTime Date { get; set; }
    }
}