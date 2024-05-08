namespace QuestEngine.Domain.Entities
{
    public class Milestone
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public double RequiredPoint { get; set; }
        public double Reward { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
