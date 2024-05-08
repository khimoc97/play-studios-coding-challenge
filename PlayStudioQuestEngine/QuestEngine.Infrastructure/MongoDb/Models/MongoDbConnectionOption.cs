namespace QuestEngine.Infrastructure.MongoDb.Models
{
    public class MongoDbConnectionOption
    {
        public const string Section = "MongoDbConnection";

        public string DefaultConnectionStr { get; set; }
        public string DatabaseName { get; set; }
    }
}
