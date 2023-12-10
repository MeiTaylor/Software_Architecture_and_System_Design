namespace Backend_IntelligentResumeAnalysisSystem.Models
{
    public class JobMatch
    {
        public int ID { get; set; }
        public string? JobTitle { get; set; }
        public int Score { get; set; }
        public string? Reason { get; set; }
        public int ApplicantProfileID { get; set; } // ForeignKey
    }
}
