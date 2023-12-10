namespace Backend_IntelligentResumeAnalysisSystem.Dtos
{
    public class JobMatchScores
    {
        public int Below60 { get; set; }
        public int Range60_70 { get; set; }
        public int Range70_80 { get; set; }
        public int Range80_90 { get; set; }
        public int Range90_100 { get; set; }
        // ...
    }

    public class JobMatchScoresInfoForGraphClass
    {
        public JobMatchScores JobMatchScores { get; set; }

    }
}
