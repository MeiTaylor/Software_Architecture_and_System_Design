using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;
using Backend_IntelligentResumeAnalysisSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend_IntelligentResumeAnalysisSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : Controller
    {
        private readonly IResumeService _resumeService;
        private readonly IApplicantService _applicantService;
        public ResumeController(IResumeService resumeService, IApplicantService applicantService)
        {
            _resumeService = resumeService;
            _applicantService = applicantService;
        }
        ///-------------以下所有的接口都是为了简历统计可视化中的图----------------

        ///简历总数（类似与外卖那个首页的总数）
        ///每个岗位多少人：简历总数，每个岗位人数（瀑布图）
        ///传入
        //{
        //  userid:
        //}
        //    返回：
        //{
        //  "totalResumes": 500,
        //  "jobResumeCounts": [
        //  {"jobName": "jobName", "resumeCount": 10},
        //  { "jobName": "jobName", "resumeCount": 20},
        //  ]
        //}
        [HttpPost("graph/total")]
        public GraphForJobResumeCountModelClass ForJobResumeCount(WebSentUserId webSentUserId)
        {
            int userId = webSentUserId.Id;//这是用户ID
            var result = _resumeService.ForJobResumeCount(userId);
            return result;
        }
        ///最高学历：高中及以下，大专，本科，硕士，博士
        ///毕业院校：985/211，普通一本，二本及以下
        ///（以上两个用嵌套环形图）
        [HttpPost("graph/education")]
        public EducationInfoForGraphClass ForGraphByEducation(WebSentUserId webSentUserId)
        {
            int userId = webSentUserId.Id;
            var result = _resumeService.ForGraphByEducation(userId);
            //查出该ID对应的关于学历的所有简历数量分布
            return result;
        }

        //年龄段：18-22，22-25，25-30，30-35，35以上（柱状图，横坐标年龄段，纵坐标个数）
        [HttpPost("graph/ageInfo")]
        public AgeInfoForGraphClass ageInfoForGraphClass(WebSentUserId webSentUserId)
        {
            int userId = webSentUserId.Id;
            //查出该ID所对饮的简历数量
            var result = _resumeService.AgeInfoForGraphClass(userId);
            return result;
        }

        /// <summary>
        /// 工作稳定性；下，中下，中，中上，上（半环形图）
        /// </summary>
        /// <param name="webSentUserId"></param>
        /// <returns></returns>
        [HttpPost("graph/workStability")]
        public WorkStabilityInfoForGraphClass WorkStabilityInfoForGraph(WebSentUserId webSentUserId)
        {
            int userId = webSentUserId.Id;
            //查出该用户所在公司的所有简历的工作稳定性数量
            var result = _resumeService.WorkStabilityInfoForGraph(userId);
            return result;
        }

        /// <summary>
        /// 人岗匹配程度得分：60以下，60-70，70-80，80-90，90-100（南丁格尔玫瑰图）
        /// </summary>
        /// <param name="webSentUserId"></param>
        /// <returns></returns>
        [HttpPost("graph/JobMatchScore")]
        public JobMatchScoresInfoForGraphClass JobMatchScoresInfoForGraph(WebSentUserId webSentUserId)
        {
            int id = webSentUserId.Id;
            //此时查数据库，查出该用户所在公司的所有简历分数排布
            var result = _applicantService.JobMatchScoresInfoForGraph(webSentUserId.Id);
            return result;
        }

        /// <summary>
        /// 所有简历，列表展示
        /// </summary>
        /// <param name="webSentUserId"></param>
        /// <returns></returns>
        [HttpPost("AllSimpleReusmes")]
        public AllSimpleResumeInfoClass ForAllSimpleResumeInfo(WebSentUserId webSentUserId)
        {
            int id = webSentUserId.Id;
            //此时查数据库，获得该用户所在公司的所有简历的简单信息
            var result = _applicantService.ForAllSimpleResumes(id);
            return result;

        }

        /// <summary>
        /// 展示的是一个简历的所有详细信息
        /// </summary>
        /// <param name="resumeId"></param>
        /// <returns></returns>
        [HttpPost("OneDetailedResumeInfo")]
        public Applicant ForOneDetailedResumeInfo(WebSentUserId resumeId)
        {
            int id = resumeId.Id;//此时的ID便是该简历的resumeID；
            var result = _applicantService.GetApplicantById(id);
            return result;
        }

    }
}
