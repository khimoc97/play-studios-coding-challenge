using Microsoft.Extensions.DependencyInjection;
using QuestEngine.Infrastructure.MongoDb.Bson;
using QuestEngine.Infrastructure.MongoDb.Models;
using QuestEngine.Infrastructure.MongoDb.Repositories;
using QuestEngine.Infrastructure.Persistence.IRepositories;

namespace QuestEngine.Infrastructure
{
    public static class InfraServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<MongoDbConnectionOption> optionConfiguration)
        {
            BsonMapperConfig.AddBsonGlobalConfig();

            services.Configure(optionConfiguration);
            services.AddMongoDb();

            return services;
        }

        private static void AddMongoDb(this IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, PlayerRepository>();
        }
    }
}
