using Backend_IntelligentResumeAnalysisSystem.Data.Repository;
using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IApplicantRepository _applicantRepository;

        public ResumeService(IResumeRepository resumeRepository,
                                        IUserRepository userRepository,
                                            IApplicantRepository applicantRepository                                                              
            )
        {
            _resumeRepository = resumeRepository;
            _userRepository = userRepository;
            _applicantRepository = applicantRepository;
        }
        public GraphForJobResumeCountModelClass ForJobResumeCount(int userId)
        {
            var company = _userRepository.GetCompanyWithUser(userId);

            if (company == null)
            {
                // Handle this error according to your business requirements
                // For example, you might want to throw an exception, or return a response indicating that the user or company was not found
                return new GraphForJobResumeCountModelClass();
            }

            var totalResumes = _resumeRepository.GetResumesByCompany(company.ID).Count();

            var jobResumeCounts = company.JobPositions.Select(jp => new JobResumeCount
            {
                JobName = jp.Title,
                ResumeCount = jp.Resumes?.Count ?? 0
            }).ToList();

            var result = new GraphForJobResumeCountModelClass
            {
                TotalResumes = totalResumes,
                JobResumeCounts = jobResumeCounts
            };

            return result;
        }
        public EducationInfoForGraphClass ForGraphByEducation(int userId)
        {
            var applicants = _applicantRepository.GetApplicantsByUserId(userId);
            var highestEducationCounts = new HighestEducation();
            var graduationSchoolsLevelCounts = new GraduationSchoolsLevel();

            foreach (Applicant applicant in applicants)
            {
                switch (applicant.HighestDegree)
                {
                    case "无":
                    case "小学":
                    case "初中":
                    case "高中":
                    case "中专":
                        highestEducationCounts.HighSchoolOrLess++;
                        break;
                    case "大专":
                        highestEducationCounts.JuniorCollege++;
                        break;
                    case "本科":
                        highestEducationCounts.Bachelor++;
                        break;
                    case "硕士":
                        highestEducationCounts.Master++;
                        break;
                    case "博士":
                        highestEducationCounts.Doctor++;
                        break;
                }

                switch (applicant.SchoolLevel)
                {
                    case "985":
                        graduationSchoolsLevelCounts._985++;
                        break;
                    case "211":
                        graduationSchoolsLevelCounts._211++;
                        break;
                    case "普通一本":
                        graduationSchoolsLevelCounts.OrdinaryFirstClass++;
                        break;
                    case "一本以下":
                        graduationSchoolsLevelCounts.SecondClassOrBelow++;
                        break;
                }
            }

            var result = new EducationInfoForGraphClass
            {
                HighestEducation = highestEducationCounts,
                GraduationSchoolsLevel = graduationSchoolsLevelCounts
            };

            return result;
        }

        public AgeInfoForGraphClass AgeInfoForGraphClass(int userId)
        {
            var applicants = _applicantRepository.GetApplicantsByUserId(userId);
            var ageGroupCounts = new AgeGroups();

            foreach (Applicant applicant in applicants)
            {
                var age = applicant.Age;
                if (age >= 18 && age < 22)
                {
                    ageGroupCounts.Age18_22++;
                }
                else if (age >= 22 && age < 25)
                {
                    ageGroupCounts.Age22_25++;
                }
                else if (age >= 25 && age < 30)
                {
                    ageGroupCounts.Age25_30++;
                }
                else if (age >= 30 && age < 35)
                {
                    ageGroupCounts.Age30_35++;
                }
                else if (age >= 35)
                {
                    ageGroupCounts.Age35AndAbove++;
                }
            }

            var result = new AgeInfoForGraphClass
            {
                AgeGroups = ageGroupCounts
            };

            return result;
        }

        public WorkStabilityInfoForGraphClass WorkStabilityInfoForGraph(int userId)
        {
            var applicants = _applicantRepository.GetApplicantsByUserId(userId);

            var workStabilityCounts = new WorkStability();

            foreach (Applicant applicant in applicants)
            {
                var workStability = applicant.WorkStability;
                switch (workStability)
                {
                    case "低":
                        workStabilityCounts.Low++;
                        break;
                    case "中下":
                        workStabilityCounts.MediumLow++;
                        break;
                    case "中":
                        workStabilityCounts.Medium++;
                        break;
                    case "中上":
                        workStabilityCounts.MediumHigh++;
                        break;
                    case "高":
                        workStabilityCounts.High++;
                        break;
                    default:
                        // handle this error according to your business requirements
                        break;
                }
            }

            var result = new WorkStabilityInfoForGraphClass
            {
                WorkStability = workStabilityCounts
            };

            return result;
        }


    }
}
