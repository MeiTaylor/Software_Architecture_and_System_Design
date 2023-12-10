using Backend_IntelligentResumeAnalysisSystem.Data.Repository;
using Backend_IntelligentResumeAnalysisSystem.Dtos;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IResumeRepository _resumeRepository;
        private readonly IJobPositionRepository _jobPositionRepository;
        //private readonly IUserRepository _userRepository;

        public CompanyService(ICompanyRepository companyRepository,
                              IResumeRepository resumeRepository,
                              IJobPositionRepository jobPositionRepository)
        {
            _companyRepository = companyRepository;
            _resumeRepository = resumeRepository;
            _jobPositionRepository = jobPositionRepository;
        }

        public InfoForHomeModelClass GetInfoForHome(int userId)
        {
            var company = _companyRepository.GetCompanyWithDetails(userId);

            if (company == null)
            {
                return new InfoForHomeModelClass(); // 或者其他的错误处理代码
            }

            var infoForHome = new InfoForHomeModelClass
            {
                TotalResumes = company.Resumes.Count(),
                TotalJobs = company.JobPositions.Count,
                // 每天新增的岗位以及简历数
                weeklyStates = GetWeeklyStates(userId),
                // 显示在该主页的所需的简历历史信息
                resumeHistory = _resumeRepository.GetResumeHistory(company.ID),
                // 岗位对应的简历数量的数组
                JobResumeCounts = _jobPositionRepository.GetJobResumeCounts(company.ID)
            };

            return infoForHome;
        }

        public HomeWeeklyState GetWeeklyStates(int userId)
        {
            var company = _companyRepository.GetCompanyWithDetails(userId);


            // 获取过去七天的日期列表，包含今天
            var dateList = Enumerable.Range(0, 7).Select(days => DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd")).ToList();

            var allJobPositions = _jobPositionRepository.GetJobPositionsByCompany(company.ID);
            var allResumes = _resumeRepository.GetResumesByCompany(company.ID);

            var weeklyStates = new HomeWeeklyState
            {
                JobCounts = dateList.Select(date => new HomeJobCount
                {
                    Date = date,
                    Count = allJobPositions
                        .Where(jp => jp.CreatedDate.ToString("yyyy-MM-dd") == date)
                        .Count()
                }).ToList(),

                resumeCounts = dateList.Select(date => new HomeResumeCount
                {
                    Date = date,
                    Count = allResumes
                        .Where(r => r.CreatedDate.ToString("yyyy-MM-dd") == date)
                        .Count()
                }).ToList(),
            };

            return weeklyStates;
        }
    }
}
