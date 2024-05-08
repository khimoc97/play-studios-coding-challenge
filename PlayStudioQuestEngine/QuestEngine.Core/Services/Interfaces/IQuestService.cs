using QuestEngine.Shared.Dtos.Request;
using QuestEngine.Shared.Dtos.Response;

namespace QuestEngine.Core.Services.Interfaces
{
    public interface IQuestService
    {
        Task<QuestProgressResponseDto> UpdateQuestProgressAsync(QuestProgressRequestDto requestDto);
        Task<QuestStateResponseDto> GetQuestStateByPlayerIdAsync(string playerId);
        Task ResetQuestProgressByPlayerIdAsync(string playerId);
    }
}
