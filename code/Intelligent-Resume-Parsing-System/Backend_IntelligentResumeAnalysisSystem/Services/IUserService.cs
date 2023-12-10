using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public interface IUserService
    {
        LoginModelClass IsLogin(string account, string password);
        RegisterModelClass CreateNewAccount(RegisterSentModel register);
    }
}
