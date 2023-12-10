namespace Backend_IntelligentResumeAnalysisSystem.Models
{
    public class Applicant
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public string? EmailAddress { get; set; } // 个人邮箱
        public string? PhoneNumber { get; set; } // 手机号
        public string? HighestDegree { get; set; } // 最高学历
        public string? School { get; set; } // 毕业院校
        public string? JobIntention { get; set; } // 求职意向岗位
        public string? Skills { get; set; } // 技能证书
        public string? Honors { get; set; } // 获奖荣誉
        public string? EducationBackground { get; set; } // 教育背景
        public string? SchoolLevel { get; set; } // 最高学历学校等级
        public string? WorkStability { get; set; } // 工作稳定性程度
        public string? WorkStabilityReason { get; set; } // 工作稳定性判断的原因
        // ... 其他已有字段 ...

        
        //public ICollection<Project> Projects { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<WorkExperience> WorkExperiences { get; set; }
        public ApplicantProfile ApplicantProfile { get; set; }
        public Resume Resume { get; set; }
    }
}
