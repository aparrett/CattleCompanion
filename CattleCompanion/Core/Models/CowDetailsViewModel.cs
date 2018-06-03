using System.Collections.Generic;

namespace CattleCompanion.Core.Models
{
    public class CowDetailsViewModel
    {
        public Cow Cow { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Cow> Children { get; set; }
        public IEnumerable<Cow> Siblings { get; set; }
        public IEnumerable<Cow> PossibleMothers { get; set; }
        public IEnumerable<Cow> PossibleFathers { get; set; }
        public IEnumerable<Cow> PossibleChildren { get; set; }
    }
}