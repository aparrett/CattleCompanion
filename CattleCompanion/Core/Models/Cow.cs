using System;
using System.Collections.Generic;
using System.Linq;

namespace CattleCompanion.Core.Models
{
    public class Cow
    {
        public int Id { get; set; }
        public string GivenId { get; set; }
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public bool IsDeceased { get; set; }
        public ICollection<CowEvent> CowEvents { get; set; }
        public virtual ICollection<Relationship> ParentRelationships { get; set; }
        public virtual ICollection<Relationship> ChildrenRelationships { get; set; }

        public Cow Mother
        {
            get
            {
                var relationship = ParentRelationships?.SingleOrDefault(p => p.Type == RelationshipType.Mother);
                return relationship?.Cow1;
            }
        }

        public Cow Father
        {
            get
            {
                var relationship = ParentRelationships?.SingleOrDefault(p => p.Type == RelationshipType.Father);
                return relationship?.Cow1;
            }
        }

        public IEnumerable<Cow> Children
        {
            get
            {
                return ChildrenRelationships?.Select(r => r.Cow2);
            }
        }
    }
}