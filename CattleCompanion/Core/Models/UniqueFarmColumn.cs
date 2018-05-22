using CattleCompanion.Core.ViewModels;
using CattleCompanion.Persistence;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CattleCompanion.Core.Models
{
    public class UniqueFarmColumn : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = new ApplicationDbContext();
            var farmViewModel = (FarmFormViewModel)validationContext.ObjectInstance;
            var farmFromDb = context.Farms.SingleOrDefault(f => f.Url == farmViewModel.Url);
            return (farmFromDb == null)
                ? ValidationResult.Success
                : new ValidationResult("This url has already been taken. Please try again.");
        }
    }
}