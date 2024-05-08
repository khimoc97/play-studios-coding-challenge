using QuestEngine.Domain.Entities;
using QuestEngine.Domain.ValueObjects;

namespace QuestEngine.Infrastructure.Persistence.Interfaces
{
    public interface IConfigurationRepository : IBaseRepository<Configuration>
    {
        Task<LevelBonusRate> GetLevelBonusRateByLevelAsync(int playerLevel);
        Task<BetBonusRate> GetBetBonusRateByAmountAsync(double betAmount);
    }
}
