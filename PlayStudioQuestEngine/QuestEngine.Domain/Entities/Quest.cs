namespace QuestEngine.Domain.Entities
{
    public class Quest
    {
        public string Id { get; set; }
        public string QuestTitle { get; set; }
        public double CurrentPoint { get; set; }
        public double TargetPoint { get; set; }
        public int NumberOfMilestones { get; set; }
        public bool IsComplete { get; set; } = false;
        public bool IsActive { get; set; }
        public ICollection<Milestone> Milestones { get; set; }
    }
}
