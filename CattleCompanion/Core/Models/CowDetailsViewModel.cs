using System.Collections.Generic;

namespace CattleCompanion.Core.Models
{
    public class CowDetailsViewModel
    {
        public Cow Cow { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}