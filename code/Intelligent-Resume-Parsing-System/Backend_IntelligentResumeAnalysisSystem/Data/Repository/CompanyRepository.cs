using Backend_IntelligentResumeAnalysisSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_IntelligentResumeAnalysisSystem.Data.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        protected readonly MyDbContext _context;
        public CompanyRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public Company GetCompanyWithDetails(int userId)
        {
            return _context.Companies
                           .Include(c => c.Users)
                           .Include(c => c.Resumes)
                           .Include(c => c.JobPositions)
                           .FirstOrDefault(c => c.Users.Any(u => u.ID == userId));
        }
        public Company GetCompanyByName(string name)
        {
            return _context.Companies.FirstOrDefault(c => c.Name == name);
        }

        public void AddCompany(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
        }
    }
}
