using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend_IntelligentResumeAnalysisSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public HomeController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("statistics")]
        public InfoForHomeModelClass GetInfoForHome(WebSentUserId webSentUserId)
        {
            int userId = webSentUserId.Id;//前端传来的userID；
                                            //此时查数据库，返回对应userID所需的主页全部信息;
            var result = _companyService.GetInfoForHome(userId);
            return result;
        }
    }

}
