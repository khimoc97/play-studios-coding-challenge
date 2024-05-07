namespace QuestEngine.Domain.Entities
{
    public class Quest
    {
        public string Id { get; set; }
        public string QuestTitle { get; set; }
        public int CurrentPoint { get; set; }
        public int TargetPoint { get; set; }
        public int NumberOfMilestones { get; set; }
        public bool IsComplete { get; set; } = false;
        public bool IsActive { get; set; }
        public ICollection<Milestone> Milestones { get; set; }
    }
}
