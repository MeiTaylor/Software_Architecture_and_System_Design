using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_IntelligentResumeAnalysisSystem.Data.Repository
{
    public class JobPositionRepository : Repository<JobPosition> , IJobPositionRepository
    {
        protected readonly MyDbContext _context;
        public JobPositionRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<JobPosition> GetJobPositionsByCompany(int companyId)
        {
            return _context.JobPositions.Where(jp => jp.CompanyID == companyId).ToList();
        }
        public List<JobResumeCount> GetJobResumeCounts(int companyId)
        {
            // 这里编写获取岗位对应的简历数量的代码
            var jobResumeCounts = _context.JobPositions
                                            .Where(jp => jp.CompanyID == companyId)
                                            .Select(jp => new JobResumeCount
                                            {
                                                JobName = jp.Title,
                                                ResumeCount = _context.Resumes.Count(r => r.JobPositionID == jp.ID)
                                            })
                                            .ToList();
            return jobResumeCounts;
        }
        public IEnumerable<JobPosition> GetJobPositionsByUserId(int userId)
        {
            return _context.JobPositions.Where(jp => jp.Company.Users.Any(u => u.ID == userId)).ToList();
        }
    }
}
