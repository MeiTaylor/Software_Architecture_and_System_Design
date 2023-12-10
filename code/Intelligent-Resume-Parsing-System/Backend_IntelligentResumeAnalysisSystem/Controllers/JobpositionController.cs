using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend_IntelligentResumeAnalysisSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobpositionController
    {
        private readonly IJobPositionService _jobPositionService;
        public JobpositionController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        [HttpPost("first")]
        public HomeToUploadResume UploadJobInfo(WebSentUserId webSentUserId)
        {
            int userId = webSentUserId.Id;//用户ID
            //此时，需要完成的是，查数据库返回所有岗位的名称以及ID；
            var result = _jobPositionService.UploadJobInfo(userId);
            return result;
        }

    }
}
