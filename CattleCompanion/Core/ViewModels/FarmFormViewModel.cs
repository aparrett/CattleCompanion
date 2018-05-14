using System.ComponentModel.DataAnnotations;

namespace CattleCompanion.Core.ViewModels
{
    public class FarmFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }
}