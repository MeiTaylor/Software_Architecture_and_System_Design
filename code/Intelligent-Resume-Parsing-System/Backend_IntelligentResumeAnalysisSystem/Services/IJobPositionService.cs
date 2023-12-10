using Backend_IntelligentResumeAnalysisSystem.Dtos;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public interface IJobPositionService 
    {
        HomeToUploadResume UploadJobInfo(int userId);
    }
}
