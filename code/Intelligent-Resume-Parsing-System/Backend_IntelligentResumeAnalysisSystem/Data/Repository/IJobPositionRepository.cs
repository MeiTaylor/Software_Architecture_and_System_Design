using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;

namespace Backend_IntelligentResumeAnalysisSystem.Data.Repository
{
    public interface IJobPositionRepository : IRepository<JobPosition>
    {
        IEnumerable<JobPosition> GetJobPositionsByCompany(int companyId);
        List<JobResumeCount> GetJobResumeCounts(int companyId);
        IEnumerable<JobPosition> GetJobPositionsByUserId(int userId);
    }
}
