namespace Backend_IntelligentResumeAnalysisSystem.Dtos
{
    public class RegisterModelClass
    {
        public string Msg { get; set; }//返回注册信息（账号是否已经存在，注册成功）
        public bool IsSuccess { get; set; }//注册成功信号
    }
}
