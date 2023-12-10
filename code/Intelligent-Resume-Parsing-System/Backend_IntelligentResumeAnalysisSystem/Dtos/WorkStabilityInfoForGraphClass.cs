namespace Backend_IntelligentResumeAnalysisSystem.Dtos
{
    public class WorkStability
    {
        public int Low { get; set; }
        public int MediumLow { get; set; }
        public int Medium { get; set; }
        public int MediumHigh { get; set; }
        public int High { get; set; }
        // ...
    }
    public class WorkStabilityInfoForGraphClass
    {
        public WorkStability WorkStability { get; set; }
    }
}
