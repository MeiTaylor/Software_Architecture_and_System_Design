namespace Backend_IntelligentResumeAnalysisSystem.Models
{
    public class JobPosition
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }  // 新增的CreatedDate属性

        // 导航属性
        public ICollection<Resume> Resumes { get; set; }
        public Company Company { get; set; }
    }
}
