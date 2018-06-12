using CattleCompanion.Core.Models;
using System;
using System.Collections.Generic;

namespace CattleCompanion.Core.Dtos
{
    public class CowDto
    {
        public int Id { get; set; }
        public string GivenId { get; set; }
        public int FarmId { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public bool IsDeceased { get; set; }
        public ICollection<CowEvent> CowEvents { get; set; }
        public ICollection<Relationship> ParentRelationships { get; set; }
        public ICollection<Relationship> ChildrenRelationships { get; set; }
    }
}