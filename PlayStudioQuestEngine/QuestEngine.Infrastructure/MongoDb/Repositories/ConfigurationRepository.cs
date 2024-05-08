using MongoDB.Driver;
using QuestEngine.Domain.Entities;
using QuestEngine.Domain.ValueObjects;
using QuestEngine.Infrastructure.Persistence.Interfaces;

namespace QuestEngine.Infrastructure.MongoDb.Repositories
{
    internal class ConfigurationRepository : BaseRepository<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(IMongoDatabase mongoDb) : base(mongoDb) { }

        public async Task<LevelBonusRate> GetLevelBonusRateByLevelAsync(int playerLevel)
        {
            var configurations = await GetAllAsync();

            var matchLevelBonus = configurations
                .First()
                .LevelBonusRates
                    .Where(lbr => lbr.Level <= playerLevel)
                    .OrderByDescending(lbr => lbr.Level)
                    .First();

            return matchLevelBonus;
        }

        public async Task<BetBonusRate> GetBetBonusRateByAmountAsync(double betAmount)
        {
            var configuration = await GetAllAsync();

            var matchBetBonus = configuration
                .First()
                .BetBonusRates
                    .Where(bbr => bbr.BetAmount <= betAmount)
                    .OrderByDescending(bbr => bbr.BetAmount)
                    .First();

            return matchBetBonus;
        }
    }
}
