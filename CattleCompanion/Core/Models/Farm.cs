using System.Collections.Generic;

namespace CattleCompanion.Core.Models
{
    public class Farm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual ICollection<Cow> Cattle { get; set; }
    }
}