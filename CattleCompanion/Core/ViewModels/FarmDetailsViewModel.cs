using CattleCompanion.Core.Models;
using System.Collections.Generic;

namespace CattleCompanion.Core.ViewModels
{
    public class FarmDetailsViewModel
    {
        public Farm Farm { get; set; }
        public IEnumerable<Cow> Cattle { get; set; }
    }
}