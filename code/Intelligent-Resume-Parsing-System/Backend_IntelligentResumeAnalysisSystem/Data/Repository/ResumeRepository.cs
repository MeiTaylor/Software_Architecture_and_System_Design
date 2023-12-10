using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Backend_IntelligentResumeAnalysisSystem.Data.Repository
{
    public class ResumeRepository : Repository<Resume> , IResumeRepository
    {
        protected readonly MyDbContext _context;
        public ResumeRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Resume> GetResumesByCompany(int companyId)
        {
            // 使用 LINQ 进行多表连接
            var resumes = from resume in _context.Resumes
                          join jobPosition in _context.JobPositions on resume.JobPositionID equals jobPosition.ID
                          where jobPosition.CompanyID == companyId
                          select resume;

            return resumes.ToList();
        }

        public List<BriefHomeResumeInfo> GetResumeHistory(int companyId)
        {
            //根据传入的CompanyId先查找Company表中包含的JobPosition的信息，再找到JobPositon各自含有所有Resume的简单信息

            // 这里编写获取显示在主页所需的简历历史信息的代码
            var history = (from resume in _context.Resumes
                           join jobPosition in _context.JobPositions on resume.JobPositionID equals jobPosition.ID
                           where jobPosition.CompanyID == companyId
                           select new BriefHomeResumeInfo
                           {
                               RId = resume.ID,
                               ResumeName = resume.Applicant.Name, // 确保 Applicant 与 Resume 正确关联
                               JobIntention = jobPosition.Title,
                               UploadDate = resume.CreatedDate.ToString("yyyy-MM-dd"),
                               HighestEducation = resume.Applicant.HighestDegree
                           }).ToList();

            return history;
        }

    }
}
