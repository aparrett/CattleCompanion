namespace CattleCompanion.Core.Models
{
    public class UserFarm
    {
        public int FarmId { get; set; }
        public string UserId { get; set; }
        public Farm Farm { get; set; }
        public ApplicationUser User { get; set; }
    }
}