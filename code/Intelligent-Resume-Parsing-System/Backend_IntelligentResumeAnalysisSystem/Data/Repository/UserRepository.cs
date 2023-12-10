using Backend_IntelligentResumeAnalysisSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_IntelligentResumeAnalysisSystem.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        protected readonly MyDbContext _context;
        public UserRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }
        public User IsLogin(string account, string password)
        {
            var user = _context.Users.FirstOrDefault(c => c.Account == account && c.Password == password);
            return user;
        }
        public User GetUserByName(string account)
        {
            return _context.Users.FirstOrDefault(c => c.Account == account);
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public Company GetCompanyWithUser(int userId)
        {
            return _context.Companies
                           .Include(c => c.JobPositions)
                           .Include(c => c.Users)
                           .FirstOrDefault(c => c.Users.Any(u => u.ID == userId));
        }
    }
}
