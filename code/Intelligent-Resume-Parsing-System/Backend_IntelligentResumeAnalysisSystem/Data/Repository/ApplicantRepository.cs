using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_IntelligentResumeAnalysisSystem.Data.Repository
{
    public class ApplicantRepository : Repository<Applicant>, IApplicantRepository
    {
        protected readonly MyDbContext _context;
        public ApplicantRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Applicant>> GetApplicantsWithSkillsAsync(string skill)
        {
            // 实现特定于Applicant的查询逻辑
            return await _context.Applicants.Where(a => a.Skills.Contains(skill)).ToListAsync();
        }

        public IEnumerable<Applicant> GetApplicantsByUserId(int userId)
        {
            var company = _context.Users
                      .Include(u => u.Company)
                      .ThenInclude(c => c.Resumes)
                      .ThenInclude(r => r.Applicant)
                      .FirstOrDefault(u => u.ID == userId)
                      ?.Company;

            if (company == null)
            {
                return new List<Applicant>();
            }

            var applicants = company.Resumes
                                    .Select(r => r.Applicant)
                                    .Where(a => a != null)
                                    .Distinct()
                                    .ToList();

            return applicants;
        }
        public Applicant GetApplicantByID (int id)
        {
            var applicant = _context.Applicants
                                    .Include(a => a.ApplicantProfile)
                                    .ThenInclude(ap => ap.JobMatches)
                                    .FirstOrDefault(a => a.ID == id);
            if (applicant == null) { return new Applicant(); }
            return applicant;
        }

        public Applicant GetApplicantWithWorkAndEduByID(int id)
        {
            var applicant = _context.Applicants
                                    .Include(a => a.WorkExperiences)
                                    .Include(a => a.Educations)
                                    .FirstOrDefault(a => a.ID == id);
            if (applicant == null) { return new Applicant(); }
            return applicant;
        }

        public PersonalCharacteristics GetPersonalCharacteristics(int id)
        {
            var applicant = _context.Applicants
                                    .Include(a => a.ApplicantProfile)
                                    .ThenInclude(ap => ap.PersonalCharacteristics)
                                    .ThenInclude(p => p.Characteristics)
                                    .FirstOrDefault(a => a.ID == id);
            return applicant.ApplicantProfile.PersonalCharacteristics;
        }
        public SkillsAndExperiences GetSkillsAndExperiences(int id)
        {
            var applicant = _context.Applicants
                                    .Include(a => a.ApplicantProfile)
                                    .ThenInclude(ap => ap.SkillsAndExperiences)
                                    .ThenInclude(p => p.Characteristics)
                                    .FirstOrDefault(a => a.ID == id);
            return applicant.ApplicantProfile.SkillsAndExperiences;
        }

        public AchievementsAndHighlights GetAchievementsAndHighlights(int id)
        {
            var applicant = _context.Applicants
                                    .Include(a => a.ApplicantProfile)
                                    .ThenInclude(ap => ap.AchievementsAndHighlights)
                                    .ThenInclude(p => p.Characteristics)
                                    .FirstOrDefault(a => a.ID == id);
            return applicant.ApplicantProfile.AchievementsAndHighlights;
        }
        /*public bool UpdateApplicant(ApplicantInfo applicantInfo)
        {
            var resume = _context.Resumes.FirstOrDefault(r => r.ID == applicantInfo.ID);

            if (resume == null) { return false; }
            var existingApplicant = _context.Applicants
                                            .Include(a => a.ApplicantProfile)
                                            .Include(a => a.WorkExperiences)
                                            .Include(a => a.Awards)
                                            .FirstOrDefault(applicant => applicant.ID == resume.ApplicantID);
        }*/

        public IEnumerable<ApplicantProfile> GetApplicantProfileWithJobMatches(int userId)
        {
            var company = _context.Users
                      .Include(u => u.Company)
                      .ThenInclude(c => c.Resumes)
                      .ThenInclude(r => r.Applicant)
                      .ThenInclude(a => a.ApplicantProfile)
                      .ThenInclude(ap => ap.JobMatches)
                      .FirstOrDefault(u => u.ID == userId)
                      ?.Company;

            if (company == null)
            {
                return   new List<ApplicantProfile>();
            }

            var applicantProfiles = company.Resumes
                                    .Select(r => r.Applicant.ApplicantProfile)
                                    .Distinct()
                                    .ToList();


            return applicantProfiles;
        }

        public IEnumerable<Applicant> GetApplicantsWithApplicantProfile(int userId)
        {
            var company = _context.Users
                      .Include(u => u.Company)
                      .ThenInclude(c => c.Resumes)
                      .ThenInclude(r => r.Applicant)
                      .ThenInclude(a => a.ApplicantProfile)
                      .ThenInclude(ap => ap.JobMatches)
                      .FirstOrDefault(u => u.ID == userId)
                      ?.Company;

            if (company == null)
            {
                return new List<Applicant>();
            }

            var applicants = company.Resumes
                                    .Select(r => r.Applicant)
                                    .Where(a => a != null)
                                    .Distinct()
                                    .ToList();

            return applicants;
        }
    }
}
