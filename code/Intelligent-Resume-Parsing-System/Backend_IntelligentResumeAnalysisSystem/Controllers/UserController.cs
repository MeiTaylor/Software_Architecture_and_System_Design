using Microsoft.AspNetCore.Mvc;
using Backend_IntelligentResumeAnalysisSystem.Services;
using Backend_IntelligentResumeAnalysisSystem.Dtos;

namespace Backend_IntelligentResumeAnalysisSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Login")]
        [HttpPost]
        public LoginModelClass Login(LoginSentModel loginSentModel)
        {

            string userName = loginSentModel.UserName;
            string password = loginSentModel.Password;

            //此时应当完成，查数据库，判断该用户是否存在，以及返回对应的信息(也就是LoginModelClass类，详情见该类的注释)
            LoginModelClass result = _userService.IsLogin(userName, password);

            return result;
        }
        [HttpPost("Register")]
        public RegisterModelClass Register(RegisterSentModel registerSentModel)
        {

            var result = _userService.CreateNewAccount(registerSentModel);
            return result;
        }
    }

}
