using System.Reflection.PortableExecutable;

namespace Backend_IntelligentResumeAnalysisSystem.Models
{
    public class Characteristic
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int Score { get; set; }
        public string? Reason { get; set; }
    }

    public class PersonalCharacteristics
    {
        public int ID { get; set; }
        public ICollection<Characteristic> Characteristics { get; set; }
        public int ApplicantProfileID { get; set; } // ForeignKey
    }

    public class SkillsAndExperiences
    {
        public int ID { get; set; }
        public ICollection<Characteristic> Characteristics { get; set; }
        public int ApplicantProfileID { get; set; } // ForeignKey
    }

    public class AchievementsAndHighlights
    {
        public int ID { get; set; }
        public ICollection<Characteristic> Characteristics { get; set; }
        public int ApplicantProfileID { get; set; } // ForeignKey
    }

}
