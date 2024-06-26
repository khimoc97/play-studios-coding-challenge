﻿using MongoDB.Driver;
using QuestEngine.Domain.Entities;
using QuestEngine.Infrastructure.Persistence.Interfaces;

namespace QuestEngine.Infrastructure.MongoDb.Repositories
{
    internal class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(IMongoDatabase mongoDb) : base(mongoDb) { }

        public async Task<Player> GetCurrentPlayerAsync()
        {
            var players = await GetAllAsync();
            return players.First();
        }
    }
}
