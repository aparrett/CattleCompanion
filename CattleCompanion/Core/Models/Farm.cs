namespace CattleCompanion.Core.Models
{
    public class Farm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [UniqueFarmColumn]
        public string Url { get; set; }
    }
}