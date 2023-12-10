namespace Backend_IntelligentResumeAnalysisSystem.Dtos
{
    /// <summary>
    /// 各年龄端的简历数量
    /// </summary>
    public class AgeInfoForGraphClass
    {
        public AgeGroups AgeGroups { get; set; }
    }

    public class AgeGroups
    {
        public int Age18_22 { get; set; }
        public int Age22_25 { get; set; }
        public int Age25_30 { get; set; }
        public int Age30_35 { get; set; }
        public int Age35AndAbove { get; set; }
        // ...
    }
}
