using QuestEngine.Infrastructure.Persistence.IRepositories;

namespace QuestEngine.Infrastructure.MongoDb.Repositories
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
    }
}
