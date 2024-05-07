using QuestEngine.Domain.Entities;
using QuestEngine.Infrastructure.Persistence.IRepositories;

namespace QuestEngine.Infrastructure.MongoDb.Repositories
{
    internal class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
    }
}
