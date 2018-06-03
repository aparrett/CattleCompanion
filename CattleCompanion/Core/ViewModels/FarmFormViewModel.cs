using CattleCompanion.Core.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CattleCompanion.Core.ViewModels
{
    public class FarmFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [UniqueFarmColumn]
        [DisplayName("Custom Url")]
        public string Url { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }
}