namespace Backend_IntelligentResumeAnalysisSystem.Dtos
{
    public class HomeToUploadResume
    {
        public List<JobInfoForUpload> UploadNeedJobsInfo { get; set; }
    }
    public class JobInfoForUpload
    {
        public string JobName { get; set; }//岗位的名称
        public int JobId { get; set; }//岗位在数据库中的ID
    }
}
