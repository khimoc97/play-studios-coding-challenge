namespace QuestEngine.API
{
    public class Urls
    {
        private const string BaseUrl = "api/";

        public static class Quest
        {
            private const string Domain = BaseUrl;

            public const string QuestProgress = Domain + "/progress";
            public const string QuestState = Domain + "/state/{playerId}";
        }
    }
}
