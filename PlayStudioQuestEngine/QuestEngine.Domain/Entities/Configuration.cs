using QuestEngine.Domain.ValueObjects;

namespace QuestEngine.Domain.Entities
{
    public class Configuration
    {
        public string Id { get; set; }
        public ICollection<BetBonusRate> BetBonusRates { get; set; }
        public ICollection<LevelBonusRate> LevelBonusRates { get; set; }
    }
}
