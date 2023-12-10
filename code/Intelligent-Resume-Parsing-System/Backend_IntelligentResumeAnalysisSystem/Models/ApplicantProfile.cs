namespace Backend_IntelligentResumeAnalysisSystem.Models
{
    public class ApplicantProfile
    {
        public int ID { get; set; }
        public int ApplicantID { get; set; }
        //public string? TalentTraits { get; set; }
        /*public string? MatchingReason { get; set; } //给出此工作稳定性判断的原因
        public int MatchingScore { get; set; }  //人岗匹配程度分数*/
        // 导航属性
        public Applicant Applicant { get; set; }

        public ICollection<JobMatch> JobMatches { get; set; }
        public PersonalCharacteristics PersonalCharacteristics { get; set; }
        public SkillsAndExperiences SkillsAndExperiences { get; set; }
        public AchievementsAndHighlights AchievementsAndHighlights { get; set; }
    }
}
