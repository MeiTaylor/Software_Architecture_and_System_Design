namespace Backend_IntelligentResumeAnalysisSystem.Dtos
{
    public class InfoForHomeModelClass
    {
        public int TotalResumes { get; set; }//该公司对应的所有简历数量
        public int TotalJobs { get; set; }//所有岗位数量
        public HomeWeeklyState weeklyStates { get; set; }//每天新增德的岗位以及简历数
        public List<BriefHomeResumeInfo> resumeHistory { get; set; }//显示在该主页的所需的简历历史信息
        public List<JobResumeCount> JobResumeCounts { get; set; }//岗位对应的简历数量的数组
    }

    public class HomeWeeklyState
    {
        public List<HomeJobCount> JobCounts { get; set; }
        public List<HomeResumeCount> resumeCounts { get; set; }
    }

    public class HomeJobCount
    {
        public string Date { get; set; }//日期
        public int Count { get; set; }//当天新增岗位数量
    }

    public class HomeResumeCount
    {
        public string Date { get; set; }//日期
        public int Count { get; set; }//当天新增岗位数量
    }

    public class BriefHomeResumeInfo
    {
        public int RId { get; set; }//简历ID
        public string? ResumeName { get; set; }//求职者名字
        public string? JobIntention { get; set; }//求职者的意向岗位
        public string? UploadDate { get; set; }//该简历上传的时间
        public string? HighestEducation { get; set; }//最高学历
    }

    public class JobResumeCount
    {
        public string? JobName { get; set; }//岗位名称
        public int ResumeCount { get; set; }//该公司的该岗位的简历数量
    }
}
