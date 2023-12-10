using Backend_IntelligentResumeAnalysisSystem.Models;

namespace Backend_IntelligentResumeAnalysisSystem.Data.Repository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Company GetCompanyWithDetails(int userId);
        Company GetCompanyByName(string account);
        void AddCompany(Company company);
    }

}
