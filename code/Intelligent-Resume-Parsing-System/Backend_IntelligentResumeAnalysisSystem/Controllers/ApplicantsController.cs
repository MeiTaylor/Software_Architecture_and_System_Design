using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;
using Backend_IntelligentResumeAnalysisSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend_IntelligentResumeAnalysisSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantsController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }
        /// 修改简历（左右两侧，左侧简历，右侧修改确认）
        [HttpPost("SecondUpload")]
        public async Task<StuatusCode> SencondUploadResume(FirstAddResumeModelClass firstAddResume)
        {
            //此时将修改后的数据，上传到数据库
            //并返回是否添加成功的状态码。
            ApplicantInfo applicantInfo = firstAddResume.applicantInfo;
            var result = new StuatusCode();
            bool status = await _applicantService.UpdateApplicant(applicantInfo);
            if (status)
            {
                result.Code = 20000;
            }
            else { result.Code = 60204; }
            return result;
        }
        [HttpPost("graph/PersonalJobMatchScore")]
        public ICollection<JobMatch> GetJobMatches(WebSentResumeId webSentResumeId)
        {
            var result = _applicantService.GetJobMatchesByResumeId(webSentResumeId.Id);
            return result;
        }
        //简历详情页可以获得这个人的匹配分数
        [HttpPost("graph/PersonalCharacteristics")]
        public PersonalCharacteristics GetPersonalCharacteristics(WebSentResumeId webSentResumeId)
        {
            var result = _applicantService.GetPersonalCharacteristics(webSentResumeId.Id);
            return result;
        }
        [HttpPost("graph/SkillsAndExperiences")]
        public SkillsAndExperiences GetSkillsAndExperiences(WebSentResumeId webSentResumeId)
        {
            var result = _applicantService.GetSkillsAndExperiences(webSentResumeId.Id);
            return result;
        }

        [HttpPost("graph/AchievementsAndHighlights")]
        public AchievementsAndHighlights GetAchievementsAndHighlights(WebSentResumeId webSentResumeId)
        {
            var result = _applicantService.GetAchievementsAndHighlights(webSentResumeId.Id);
            return result;
        }

    }
}