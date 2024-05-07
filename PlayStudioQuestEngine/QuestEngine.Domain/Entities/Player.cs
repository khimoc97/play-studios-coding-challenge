namespace QuestEngine.Domain.Entities
{
    public class Player
    {
        public string Id { get; set; }
        public string PlayerName { get; set; }

        public ICollection<Quest> Quests { get; set; }
    }
}
