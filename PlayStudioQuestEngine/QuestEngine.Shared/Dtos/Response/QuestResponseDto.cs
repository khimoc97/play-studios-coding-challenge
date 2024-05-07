namespace QuestEngine.Shared.Dtos.Response
{
    public class QuestProgressResponseDto
    {
        public double QuestPointsEarned { get; set; }
        public double TotalQuestPercentCompleted { get; set; }
        public List<MilestoneResponseDto> MilestonesCompleted { get; set; }
    }

    public class QuestStateResponseDto
    {
        public double TotalQuestPercentCompleted { get; set; }
        public int LastMilestoneIndexCompleted { get; set; }
    }
}
