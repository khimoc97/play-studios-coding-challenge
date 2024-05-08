using QuestEngine.Core.Services.Interfaces;
using QuestEngine.Infrastructure.Persistence.Interfaces;
using QuestEngine.Shared.Dtos.Response;

namespace QuestEngine.Core.Services.Implementations
{
    internal class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PlayerResponseDto> GetCurrentPlayerAsync()
        {
            var player = await _playerRepository.GetCurrentPlayerAsync();

            return new PlayerResponseDto
            {
                Id = player.Id,
                PlayerName = player.PlayerName
            };
        }
    }
}
