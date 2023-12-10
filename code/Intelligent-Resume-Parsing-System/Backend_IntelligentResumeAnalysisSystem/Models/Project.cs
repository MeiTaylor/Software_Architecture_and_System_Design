namespace Backend_IntelligentResumeAnalysisSystem.Models
{
    public class Project
    {
        public int ID { get; set; }
        public int ApplicantID { get; set; }
        public string? ProjectName { get; set; }
        public string? Job { get; set; }
        public string? Describe { get; set; }
        public string? Duty {  get; set; }
        public string? Location { get; set; }
        public string? StartTime { get; set; } //开始年份-月份，若只有年则只输出年。如2019-10或2019
        public string? EndTime { get; set; }  //结束年份-月份
        public string? IsIn { get; set; } //是否仍在：1表示在，0表示不在
    }
}
