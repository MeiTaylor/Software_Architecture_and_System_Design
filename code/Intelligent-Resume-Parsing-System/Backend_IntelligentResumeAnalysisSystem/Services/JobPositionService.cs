using Backend_IntelligentResumeAnalysisSystem.Data.Repository;
using Backend_IntelligentResumeAnalysisSystem.Dtos;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public class JobPositionService : IJobPositionService
    {
        readonly private IJobPositionRepository _jobPositionRepository;

        public JobPositionService(IJobPositionRepository jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }

        public HomeToUploadResume UploadJobInfo(int userId)
        {
            var jobpositions = _jobPositionRepository.GetJobPositionsByUserId(userId);

            if (jobpositions == null)
            {
                // 如果找不到对应的公司，返回错误信息
                return new HomeToUploadResume { UploadNeedJobsInfo = new List<JobInfoForUpload>() };
            }


            // 获取该公司下所有岗位的信息
            var jobsInfo = jobpositions.Select(jobposition => new JobInfoForUpload
            {
                JobName = jobposition.Title,
                JobId = jobposition.ID
            }).ToList();

            return new HomeToUploadResume { UploadNeedJobsInfo = jobsInfo };
        }

    }
}
