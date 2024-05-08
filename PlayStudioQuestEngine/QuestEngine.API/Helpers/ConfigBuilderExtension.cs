namespace QuestEngine.API.Helpers
{
    public class ConfigBuilderExtension
    {
        public static IConfigurationBuilder CreateConfigBuilder(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrEmpty(env))
            {
                env = Environment.GetEnvironmentVariable("DOTNETCORE_ENVIRONMENT");
            }

            if (string.IsNullOrEmpty(env))
            {
                env = "Development";
            }

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);
        }
    }
}
