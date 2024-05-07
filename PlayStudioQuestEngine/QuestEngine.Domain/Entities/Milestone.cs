namespace QuestEngine.Domain.Entities
{
    public class Milestone
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public int RequiredPoint { get; set; }
        public int Reward { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
