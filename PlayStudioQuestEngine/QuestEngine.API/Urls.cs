namespace QuestEngine.API
{
    public class Urls
    {
        private const string BaseUrl = "/api";

        public static class Player
        {
            private const string Domain = BaseUrl + "/player";

            public const string GetCurrentPlayer = Domain;
        }

        public static class Quest
        {
            private const string Domain = BaseUrl;

            public const string ResetQuestProgress = Domain + "/reset/{playerId}";
            public const string QuestProgress = Domain + "/progress";
            public const string QuestState = Domain + "/state/{playerId}";
        }
    }
}
