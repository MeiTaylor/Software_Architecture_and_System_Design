namespace Backend_IntelligentResumeAnalysisSystem.Models
{
    public class Education
    {
        public int ID { get; set; }
        public int ApplicantID { get; set; }
        public string? School { get; set; }
        public string? College { get; set; }
        public string? Major { get; set; }
        public string? SchoolingRecord { get; set; } //学历，输出统一为：博士研究生/硕士研究生/本科/专科/高中/中专/初中
        public string? Degree { get; set; } //学位，输出统一为:博士/硕士/学士
        public string? StartTime { get; set; } //开始年份-月份，若只有年则只输出年。如2019-10或2019
        public string? EndTime { get; set; }  //结束年份-月份
        public string? IsIn {  get; set; } //是否仍在校：1表示在，0表示不在
        public string? Gpa { get; set; }
        public string? Rank { get; set; }
    }
}
