namespace Backend_IntelligentResumeAnalysisSystem.Models
{
    public class WorkExperience
    {
        public int ID { get; set; }
        public int ApplicantID { get; set; }
        public string? Company { get; set; }
        public string? Location { get; set; }
        public string? Job { get; set; }
        public string? Package { get; set; } //该岗位工资水平，如，3000-5000元/月，输出为原文
        public string? StartTime { get; set; } //开始年份-月份，若只有年则只输出年。如2019-10或2019
        public string? EndTime { get; set; }  //结束年份-月份
        public string? IsIn { get; set; } //是否仍在：1表示在，0表示不在
        public string? Describe { get; set; } //工作内容
    }
}
