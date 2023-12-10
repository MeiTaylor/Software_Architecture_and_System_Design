using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;

namespace Backend_IntelligentResumeAnalysisSystem.Data.Repository
{
    public interface IResumeRepository : IRepository<Resume>
    {
        List<BriefHomeResumeInfo> GetResumeHistory(int iD);
        IEnumerable<Resume> GetResumesByCompany(int companyId);
    }
}
