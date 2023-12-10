namespace Backend_IntelligentResumeAnalysisSystem.Models
{
    public class User
    {
        public int ID { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }  // 用户的角色，例如 "admin", "normal" 等
        public int CompanyID { get; set; }  // 外键，连接到Company表的ID
        public Company Company { get; set; }  // 导航属性，用于ORM的关联查询
    }
}
