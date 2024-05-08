using QuestEngine.API.Helpers;
using QuestEngine.Core;
using QuestEngine.Core.Services.Interfaces;
using QuestEngine.Infrastructure;
using Serilog;
using Serilog.Extensions.Logging;
using System.Text.Json.Serialization;

const string appName = "PlayStudio.QuestEngine.API";

var serilogConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(ConfigBuilderExtension.CreateConfigBuilder(args).Build());

Log.Logger = serilogConfig.CreateLogger();
var serilogLoggerFactory = new SerilogLoggerFactory(Log.Logger);

Console.Title = appName;
var logger = Log.Logger;

try
{
    logger.Debug($"Starting up...{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog(logger);

    // Add services to the container.
    await ConfigureServices(builder.Services, builder.Configuration);

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    // Seed data
    using (var scope = app.Services.CreateScope())
    {
        var initDataService = scope.ServiceProvider.GetRequiredService<IInitDataService>();
        await initDataService.CreateInitDataAsync();
    }

    // Start.
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "{AppName} start-up failed {Message}", appName, ex.Message);
}
finally
{
    Log.CloseAndFlush();
}

Task ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.Configure<RouteOptions>(option => option.LowercaseUrls = true);
    services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
    services.AddLogging(builder =>
    {
        var logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();

        builder.AddSerilog(logger);
    });
    services.AddMemoryCache();
    services.AddHttpContextAccessor();

    // Core services.
    services.AddCoreServices();

    // Infrastructure services.
    services.AddInfrastructureServices(configuration);

    return Task.CompletedTask;
}