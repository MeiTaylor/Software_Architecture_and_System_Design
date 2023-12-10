
using Backend_IntelligentResumeAnalysisSystem.interfaces;
using Backend_IntelligentResumeAnalysisSystem.parsers;

namespace Backend_IntelligentResumeAnalysisSystem.factories
{
    public class ResumeParserFactory : IResumeParserFactory
    {
        public IResumeParser CreateParser(string parserType)
        {
            switch (parserType.ToLower())
            {
                case "basicinfo":
                    return new BasicInfoParser();
                case "talentprofile":
                    return new TalentProfileParser();
                case "jobmatch":
                    return new JobMatchParser();
                case "educationandworkexperience":
                    return new EducationAndWorkexperienceParser();
                default:
                    throw new ArgumentException("不支持的简历分析类型");
            }
        }
    }

}
