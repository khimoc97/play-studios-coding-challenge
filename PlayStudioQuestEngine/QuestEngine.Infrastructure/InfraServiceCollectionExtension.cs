using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using QuestEngine.Infrastructure.MongoDb.Bson;
using QuestEngine.Infrastructure.MongoDb.Models;
using QuestEngine.Infrastructure.MongoDb.Repositories;
using QuestEngine.Infrastructure.Persistence.Interfaces;

namespace QuestEngine.Infrastructure
{
    public static class InfraServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            BsonMapperConfig.AddBsonGlobalConfig();

            services.AddMongoDb(configuration);

            return services;
        }

        private static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbConnection = configuration.GetSection(MongoDbConnectionOption.Section)
                .Get<MongoDbConnectionOption>() ?? throw new Exception("Missing MongoDb connection options.");

            var mongoDb = new MongoClient(mongoDbConnection.DefaultConnectionStr)
                .GetDatabase(mongoDbConnection.DatabaseName);

            services.AddSingleton(mongoDb);
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
        }
    }
}
