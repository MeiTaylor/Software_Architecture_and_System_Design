using Backend_IntelligentResumeAnalysisSystem.Models;

namespace Backend_IntelligentResumeAnalysisSystem.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User IsLogin(string account, string password);
        Company GetCompanyWithUser(int userId);
        User GetUserByName(string account);
        void AddUser(User user);
    }
}
