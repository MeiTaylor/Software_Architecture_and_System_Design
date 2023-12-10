using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public interface IApplicantService
    {
        Task<bool> UpdateApplicant(ApplicantInfo applicantInfo);
        AllSimpleResumeInfoClass ForAllSimpleResumes(int userId);
        Applicant GetApplicantById(int id);
        ICollection<JobMatch> GetJobMatchesByResumeId(int resumeId);
        PersonalCharacteristics GetPersonalCharacteristics(int id);
        SkillsAndExperiences GetSkillsAndExperiences(int id);
        AchievementsAndHighlights GetAchievementsAndHighlights(int id);
        JobMatchScoresInfoForGraphClass JobMatchScoresInfoForGraph(int resumeId);
    }
}
