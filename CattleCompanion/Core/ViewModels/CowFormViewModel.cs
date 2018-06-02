using CattleCompanion.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CattleCompanion.Core.ViewModels
{
    public class CowFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name or Id")]
        public string GivenId { get; set; }

        [Required]
        [DisplayName("Farm")]
        public int FarmId { get; set; }

        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        public IEnumerable<Farm> Farms { get; set; }
    }
}