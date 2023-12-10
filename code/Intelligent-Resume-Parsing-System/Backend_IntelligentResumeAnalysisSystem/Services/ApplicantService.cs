using Backend_IntelligentResumeAnalysisSystem.Data.Repository;
using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantrepository;

        public ApplicantService(IApplicantRepository applicantrepository)
        {
            _applicantrepository = applicantrepository;
        }
        public async Task<bool> UpdateApplicant(ApplicantInfo applicantInfo)
        {
            var applicant = await _applicantrepository.GetByIdAsync(applicantInfo.ID);
            if (applicant == null) { return false; }
            // 更新基本属性
            applicant.Name = applicantInfo.Name ?? applicant.Name;
            applicant.Gender = applicantInfo.Gender ?? applicant.Gender;
            applicant.Age = applicantInfo.Age; // 假设年龄总是有值
            applicant.EmailAddress = applicantInfo.EmailAddress ?? applicant.EmailAddress;
            applicant.PhoneNumber = applicantInfo.PhoneNumber ?? applicant.PhoneNumber;
            applicant.HighestDegree = applicantInfo.HighestDegree ?? applicant.HighestDegree;
            applicant.School = applicantInfo.School ?? applicant.School;
            applicant.JobIntention = applicantInfo.JobIntention ?? applicant.JobIntention;
            applicant.Skills = applicantInfo.Skills ?? applicant.Skills;
            applicant.Honors = applicantInfo.Honors ?? applicant.Honors;
            applicant.EducationBackground = applicantInfo.EducationBackground ?? applicant.EducationBackground;
            applicant.SchoolLevel = applicantInfo.SchoolLevel ?? applicant.SchoolLevel;
            applicant.WorkStability = applicantInfo.WorkStability ?? applicant.WorkStability;
            applicant.WorkStabilityReason = applicantInfo.WorkStabilityReason ?? applicant.WorkStabilityReason;

            // 保存更新
            await _applicantrepository.UpdateAsync(applicant);
            return true;
        }
        public AllSimpleResumeInfoClass ForAllSimpleResumes(int userId)
        {
            var applicants = _applicantrepository.GetApplicantsWithApplicantProfile(userId);

            // 将每个Applicant对象转换为SimpleResume对象
            var simpleResumeList = applicants.Select(applicant => new SimpleResume
            {
                Rid = applicant.ID,
                Name = applicant.Name,
                Age = applicant.Age,
                HighestEducation = applicant.HighestDegree,
                PhoneNumber = applicant.PhoneNumber,
                JobIntention = applicant.JobIntention,
                Gender = applicant.Gender,
                MaxMatchingScore = applicant.ApplicantProfile.JobMatches.Max(jm => jm.Score)
            }).ToList();

            // 创建AllSimpleResumes对象并返回
            var allSimpleResumeInfo = new AllSimpleResumeInfoClass
            {
                SimpleResumes = simpleResumeList,
                Code = 20000
            };

            return allSimpleResumeInfo;
        }
        public Applicant GetApplicantById(int id)
        {
            return _applicantrepository.GetApplicantWithWorkAndEduByID(id);
        }
        public ICollection<JobMatch> GetJobMatchesByResumeId(int resumeId)
        {
            // 首先，我们需要找到与给定resumeId对应的Applicant
            var applicant = _applicantrepository.GetApplicantByID(resumeId);

            // 如果找不到applicant或者其对应的ApplicantProfile，我们返回一个空的列表
            if (applicant == null || applicant.ApplicantProfile == null)
            {
                return new List<JobMatch>();
            }

            // 最后，返回applicantProfile的JobMatches
            return applicant.ApplicantProfile.JobMatches;
        }
        public PersonalCharacteristics GetPersonalCharacteristics(int resumeId)
        {
            var personalcharacteristics = _applicantrepository.GetPersonalCharacteristics(resumeId);
            if (personalcharacteristics == null)
            {
                throw new Exception("Applicant Personalcharacteristics not found");
            }

            return personalcharacteristics;
        }
        public SkillsAndExperiences GetSkillsAndExperiences(int resumeId)
        {
            var skillsandexperiences = _applicantrepository.GetSkillsAndExperiences(resumeId);
            if (skillsandexperiences == null)
            {
                throw new Exception("Applicant Skillsandexperiences not found");
            }

            return skillsandexperiences;
        }
        public AchievementsAndHighlights GetAchievementsAndHighlights(int resumeId)
        {
            var achievementsandhighlights = _applicantrepository.GetAchievementsAndHighlights(resumeId);
            if (achievementsandhighlights == null)
            {
                throw new Exception("Applicant Achievementsandhighlights not found");
            }

            return achievementsandhighlights;
        }
        public JobMatchScoresInfoForGraphClass JobMatchScoresInfoForGraph(int userId)
        {
            var applicantProfiles = _applicantrepository.GetApplicantProfileWithJobMatches(userId);

            if (applicantProfiles == null)
            {
                throw new Exception("ApplicantProfiles not found");
            }

            var jobMatchScores = new JobMatchScores();
            foreach (var applicantProfile in applicantProfiles)
            {
                // 检查JobMatches是否不为空
                if (applicantProfile.JobMatches != null && applicantProfile.JobMatches.Any())
                {
                    // 找到每个JobMatches集合中的最大Score并添加到列表中
                    int maxScore = applicantProfile.JobMatches.Max(jm => jm.Score);
                    if (maxScore < 60)
                    {
                        jobMatchScores.Below60++;
                    }
                    else if (maxScore < 70)
                    {
                        jobMatchScores.Range60_70++;
                    }
                    else if (maxScore < 80)
                    {
                        jobMatchScores.Range70_80++;
                    }
                    else if (maxScore < 90)
                    {
                        jobMatchScores.Range80_90++;
                    }
                    else
                    {
                        jobMatchScores.Range90_100++;
                    }
                }
            }

            return new JobMatchScoresInfoForGraphClass { JobMatchScores = jobMatchScores };
        }

    }
}
