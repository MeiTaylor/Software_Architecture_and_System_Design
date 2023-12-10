namespace Backend_IntelligentResumeAnalysisSystem.Dtos
{
    public class LoginModelClass
    {
        public string? Data { get; set; }//这个用来标识用户权限，为admin则是普通用户，为editor则为创建者用户
        public int Code { get; set; }//登录成功状态码,20000登录成功，60204登录失败
        public int UserId { get; set; }//用户ID
    }
}
