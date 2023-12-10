using Backend_IntelligentResumeAnalysisSystem.Dtos;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public interface IResumeService
    {
        GraphForJobResumeCountModelClass ForJobResumeCount(int userId);
        EducationInfoForGraphClass ForGraphByEducation(int userId);
        AgeInfoForGraphClass AgeInfoForGraphClass(int userId);
        WorkStabilityInfoForGraphClass WorkStabilityInfoForGraph(int userId);
    }
}
