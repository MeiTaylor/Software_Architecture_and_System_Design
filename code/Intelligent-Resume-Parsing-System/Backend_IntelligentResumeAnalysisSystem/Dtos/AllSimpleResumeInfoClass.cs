namespace Backend_IntelligentResumeAnalysisSystem.Dtos
{
    public class AllSimpleResumeInfoClass
    {
        public int Code { get; set; }
        public List<SimpleResume> SimpleResumes { get; set; }
    }

    public class SimpleResume
    {
        public int Rid { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string HighestEducation { get; set; }
        public string PhoneNumber { get; set; }
        public string JobIntention { get; set; }
        public string Gender { get; set; }
        public int MaxMatchingScore { get; set; }
    }
}
