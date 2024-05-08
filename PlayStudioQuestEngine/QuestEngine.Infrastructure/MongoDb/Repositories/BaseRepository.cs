using MongoDB.Driver;
using QuestEngine.Infrastructure.Persistence.Interfaces;

namespace QuestEngine.Infrastructure.MongoDb.Repositories
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _entityCollection;

        public BaseRepository(IMongoDatabase mongoDb)
        {
            _entityCollection = mongoDb.GetCollection<T>(typeof(T).Name);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _entityCollection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var result = await _entityCollection.Find(FilterById(id)).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _entityCollection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async virtual Task<T> UpdateByIdAsync(string id, T entity)
        {
            await _entityCollection.ReplaceOneAsync(FilterById(id), entity);
            return entity;
        }


        public async Task<bool> CheckSeedingDataExist()
        {
            var result = await _entityCollection.Find(Builders<T>.Filter.Empty).ToListAsync();
            return result.Count != 0;
        }

        protected static FilterDefinition<T> FilterById(string key)
        {
            return Builders<T>.Filter.Eq("Id", key);
        }
    }
}
