using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using QuestEngine.Core.Services.Interfaces;
using QuestEngine.Domain.Entities;
using QuestEngine.Domain.ValueObjects;
using QuestEngine.Infrastructure.Persistence.Interfaces;

namespace QuestEngine.Core.Services.Implementations
{
    internal class InitDataService : IInitDataService
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<InitDataService> _logger;

        public InitDataService(
            IConfigurationRepository configurationRepository,
            IPlayerRepository playerRepository,
            ILogger<InitDataService> logger)
        {
            _configurationRepository = configurationRepository;
            _playerRepository = playerRepository;
            _logger = logger;
        }

        public async Task CreateInitDataAsync()
        {
            await SeedConfigurationDataAsync();
            await SeedPlayerDataAsync();
        }

        private async Task SeedConfigurationDataAsync()
        {
            if (await _configurationRepository.CheckSeedingDataExist())
            {
                _logger.LogInformation("Configuration data already seeded.");
                return;
            }

            var configuration = new Configuration
            {
                BetBonusRates = BetBonusRatesData(),
                LevelBonusRates = LevelBonusRatesData()
            };

            await _configurationRepository.AddAsync(configuration);
            _logger.LogInformation($"Configuration data seeded with: {configuration.LevelBonusRates.Count} level bonus step, {configuration.BetBonusRates.Count} bet bonus step");
        }

        private async Task SeedPlayerDataAsync()
        {
            if (await _playerRepository.CheckSeedingDataExist())
            {
                _logger.LogInformation("Player data already seeded.");
                return;
            }

            var player = new Player
            {
                PlayerName = "Player Khanh",
                Quests = [QuestData()]
            };

            await _playerRepository.AddAsync(player);
            _logger.LogInformation($"Player data seeded with: player's name {player.PlayerName}, {player.Quests.Count} quests. Created with Id: {player.Id}");
        }

        private List<BetBonusRate> BetBonusRatesData()
        {
            var betBonusRates = new List<BetBonusRate>
            {
                new() {
                    BetAmount = 1,
                    Rate = 1.0
                },
                new() {
                    BetAmount = 1000,
                    Rate = 1.1
                },
                new() {
                    BetAmount = 2000,
                    Rate = 1.2
                },
                new() {
                    BetAmount = 3000,
                    Rate = 1.3
                },
                new() {
                    BetAmount = 4000,
                    Rate = 1.4
                },
                new() {
                    BetAmount = 5000,
                    Rate = 1.5
                }
            };
            return betBonusRates;
        }

        private List<LevelBonusRate> LevelBonusRatesData()
        {
            var levelBonusRates = new List<LevelBonusRate>
            {
                new()
                {
                    Level = 0,
                    Rate = 1.0
                },
                new()
                {
                    Level = 5,
                    Rate = 1.1
                },
                new()
                {
                    Level = 10,
                    Rate = 1.2
                },
                new()
                {
                    Level = 15,
                    Rate = 1.3
                },
                new()
                {
                    Level = 20,
                    Rate = 1.4
                },
                new()
                {
                    Level = 25,
                    Rate = 1.5
                }
            };

            return levelBonusRates;
        }

        private Quest QuestData()
        {
            var quest = new Quest
            {
                Id = ObjectId.GenerateNewId().ToString(),
                QuestTitle = "Player Khanh's Quest",
                CurrentPoint = 0,
                TargetPoint = 35000,
                IsActive = true,
                IsComplete = false,
                NumberOfMilestones = 5,
                Milestones =
                [
                    new()
                    {
                        Index = 0,
                        Title = "Milestone 1",
                        RequiredPoint = 6000,
                        IsComplete = false,
                        Reward = 1000
                    },
                    new()
                    {
                        Index = 1,
                        Title = "Milestone 2",
                        RequiredPoint = 12000,
                        IsComplete = false,
                        Reward = 1500
                    },
                    new()
                    {
                        Index = 2,
                        Title = "Milestone 3",
                        RequiredPoint = 18000,
                        IsComplete = false,
                        Reward = 2000
                    },
                    new()
                    {
                        Index = 3,
                        Title = "Milestone 4",
                        RequiredPoint = 24000,
                        IsComplete = false,
                        Reward = 2500
                    },
                    new()
                    {
                        Index = 4,
                        Title = "Milestone 5",
                        RequiredPoint = 30000,
                        IsComplete = false,
                        Reward = 1000
                    }
                ]
            };

            return quest;
        }
    }
}
