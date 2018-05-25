﻿using System;

namespace CattleCompanion.Core.Models
{
    public class Cow
    {
        public int Id { get; set; }
        public string GivenId { get; set; }
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
        public int MotherId { get; set; }
        public int FatherId { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public bool IsDeceased { get; set; }
    }
}