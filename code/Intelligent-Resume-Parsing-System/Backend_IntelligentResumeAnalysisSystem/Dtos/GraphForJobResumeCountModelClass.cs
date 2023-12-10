namespace Backend_IntelligentResumeAnalysisSystem.Dtos
{
    public class GraphForJobResumeCountModelClass
    {
        public int TotalResumes { get; set; }//所有简历数
        public List<JobResumeCount> JobResumeCounts { get; set; }
    }
}
