using QuestEngine.Shared.Dtos.Response;

namespace QuestEngine.Core.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerResponseDto> GetCurrentPlayerAsync();
    }
}
