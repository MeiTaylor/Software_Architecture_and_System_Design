using Backend_IntelligentResumeAnalysisSystem.Models;

namespace Backend_IntelligentResumeAnalysisSystem.Data.Repository
{
    public interface IApplicantRepository : IRepository<Applicant>
    {
        Task<IEnumerable<Applicant>> GetApplicantsWithSkillsAsync(string skill);
        // 其他特定于Applicant的方法
        IEnumerable<Applicant> GetApplicantsByUserId(int userId);
        Applicant GetApplicantByID(int id);
        PersonalCharacteristics GetPersonalCharacteristics(int id);
        SkillsAndExperiences GetSkillsAndExperiences(int id);
        AchievementsAndHighlights GetAchievementsAndHighlights(int id);
        IEnumerable<ApplicantProfile> GetApplicantProfileWithJobMatches(int userId);
        IEnumerable<Applicant> GetApplicantsWithApplicantProfile(int userId);
        Applicant GetApplicantWithWorkAndEduByID(int id);

    }
}
