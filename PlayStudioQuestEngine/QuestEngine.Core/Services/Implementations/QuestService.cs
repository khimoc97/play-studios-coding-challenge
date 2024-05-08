using Microsoft.Extensions.Logging;
using QuestEngine.Core.Exceptions;
using QuestEngine.Core.Services.Interfaces;
using QuestEngine.Domain.Entities;
using QuestEngine.Infrastructure.Persistence.Interfaces;
using QuestEngine.Shared.Dtos.Request;
using QuestEngine.Shared.Dtos.Response;

namespace QuestEngine.Core.Services.Implementations
{
    internal class QuestService : IQuestService
    {
        private readonly ILogger<QuestService> _logger;
        private readonly IPlayerRepository _playerRepository;
        private readonly IConfigurationRepository _configurationRepository;

        public QuestService(
            ILogger<QuestService> logger,
            IPlayerRepository playerRepository,
            IConfigurationRepository configurationRepository)
        {
            _logger = logger;
            _playerRepository = playerRepository;
            _configurationRepository = configurationRepository;
        }

        public async Task<QuestProgressResponseDto> UpdateQuestProgressAsync(QuestProgressRequestDto requestDto)
        {
            if (requestDto.ChipAmountBet <= 0)
                throw new BadRequestException("Chip amount bet must be greater than 0.");

            if (requestDto.PlayerLevel < 0)
                throw new BadRequestException("Player level must be greater or equal 0.");

            var player = await _playerRepository.GetPlayerByIdAsync(requestDto.PlayerId)
                ?? throw new BadRequestException($"Player's Id: {requestDto.PlayerId} does not exist.");

            if (player.Quests.Count == 0 || player.Quests.All(quest => quest.IsComplete))
                throw new BadRequestException($"Player {requestDto.PlayerId} currently doesn't have any quest or all quests have been completed.");

            if (player.Quests.Count(quest => quest.IsActive) > 1)
                throw new BadRequestException($"Player {requestDto.PlayerId} currently has more than one active quest. Please check data again!");

            var currentActiveQuest = player.Quests.Single(quest => quest.IsActive);

            var earnableQuestPoint = await CalculateEarnableQuestPoint(requestDto.PlayerLevel, requestDto.ChipAmountBet);
            currentActiveQuest.CurrentPoint += earnableQuestPoint;

            CheckQuestMileStones(currentActiveQuest);

            if (currentActiveQuest.CurrentPoint >= currentActiveQuest.TargetPoint)
            {
                currentActiveQuest.IsComplete = true;
                currentActiveQuest.IsActive = false;
            }

            // Update player's quest progress to database.
            await _playerRepository.UpdatePlayerByIdAsync(player.Id, player);

            var questCompletedPercentage = Math.Round(currentActiveQuest.CurrentPoint / currentActiveQuest.TargetPoint * 100, 2);
            var result = new QuestProgressResponseDto
            {
                MilestonesCompleted = currentActiveQuest.Milestones
                    .Where(milestone => milestone.IsComplete)
                    .Select(milestone => new MilestoneResponseDto
                    {
                        MilestoneIndex = milestone.Index,
                        ChipsAwarded = milestone.Reward
                    })
                    .OrderBy(milestone => milestone.MilestoneIndex)
                    .ToList(),
                QuestPointsEarned = earnableQuestPoint,
                TotalQuestPercentCompleted = questCompletedPercentage > 100 ? 100 : questCompletedPercentage
            };

            return result;
        }

        public async Task<QuestStateResponseDto> GetQuestStateByPlayerIdAsync(string playerId)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(playerId)
                ?? throw new BadRequestException($"Player's Id: {playerId} does not exist.");

            if (player.Quests.Count == 0 || player.Quests.All(quest => quest.IsComplete))
                throw new BadRequestException($"Player {playerId} currently doesn't have any quest or all quests have been completed.");

            if (player.Quests.Count(quest => quest.IsActive) > 1)
                throw new BadRequestException($"Player {playerId} currently has more than one active quest. Please check data again!");

            var currentQuest = player.Quests.Single(quest => quest.IsActive);
            var completedMilestones = currentQuest.Milestones.Where(milestone => milestone.IsComplete).ToList();
            var currentcompletePercentage = Math.Round(currentQuest.CurrentPoint / currentQuest.TargetPoint * 100, 2);

            return new QuestStateResponseDto
            {
                TotalQuestPercentCompleted = currentcompletePercentage > 100 ? 100 : currentcompletePercentage,
                LastMilestoneIndexCompleted = completedMilestones.Count != 0 ? completedMilestones.Max(milestone => milestone.Index) : -1,
            };
        }

        private void CheckQuestMileStones(Quest currentQuest)
        {
            var completableMilestones = currentQuest.Milestones
                .Where(milestone => milestone.IsComplete == false && milestone.RequiredPoint <= currentQuest.CurrentPoint)
                .ToList();

            completableMilestones.ForEach(milestone =>
            {
                milestone.IsComplete = true;
            });
        }

        private async Task<double> CalculateEarnableQuestPoint(int playerLevel, double betAmount)
        {
            var levelBonus = await _configurationRepository.GetLevelBonusRateByLevelAsync(playerLevel);
            var betBonus = await _configurationRepository.GetBetBonusRateByAmountAsync(betAmount);

            return (playerLevel * levelBonus.Rate) + (betAmount * betBonus.Rate);
        }
    }
}
