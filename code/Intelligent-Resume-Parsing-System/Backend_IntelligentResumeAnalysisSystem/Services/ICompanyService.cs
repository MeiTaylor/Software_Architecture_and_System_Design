using Backend_IntelligentResumeAnalysisSystem.Dtos;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public interface ICompanyService
    {
        InfoForHomeModelClass GetInfoForHome(int userId);
        //HomeWeeklyState GetWeeklyStates(int userId);

    }
}
