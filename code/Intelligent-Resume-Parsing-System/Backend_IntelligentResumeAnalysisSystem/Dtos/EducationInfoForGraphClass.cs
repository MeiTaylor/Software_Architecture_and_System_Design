namespace Backend_IntelligentResumeAnalysisSystem.Dtos
{
    public class EducationInfoForGraphClass
    {
        public HighestEducation HighestEducation { get; set; }
        public GraduationSchoolsLevel GraduationSchoolsLevel { get; set; }
    }

    public class HighestEducation
    {
        public int HighSchoolOrLess { get; set; }
        public int JuniorCollege { get; set; }
        public int Bachelor { get; set; }
        public int Master { get; set; }
        public int Doctor { get; set; }
        // ...
    }

    public class GraduationSchoolsLevel
    {
        public int _985 { get; set; }
        public int _211 { get; set; }
        public int OrdinaryFirstClass { get; set; }
        public int SecondClassOrBelow { get; set; }
        // ...
    }
}
