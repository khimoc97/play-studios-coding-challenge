using QuestEngine.Domain.Entities;

namespace QuestEngine.Infrastructure.Persistence.Interfaces
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        Task<Player> GetPlayerByIdAsync(string id) => GetByIdAsync(id);
        Task<Player> UpdatePlayerByIdAsync(string id, Player player) => UpdateByIdAsync(id, player);
    }
}
