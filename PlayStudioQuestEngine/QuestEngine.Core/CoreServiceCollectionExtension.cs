using Microsoft.Extensions.DependencyInjection;
using QuestEngine.Core.Services.Implementations;
using QuestEngine.Core.Services.Interfaces;

namespace QuestEngine.Core
{
    public static class CoreServiceCollectionExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IInitDataService, InitDataService>();
            services.AddScoped<IQuestService, QuestService>();
            services.AddScoped<IPlayerService, PlayerService>();

            return services;
        }
    }
}
