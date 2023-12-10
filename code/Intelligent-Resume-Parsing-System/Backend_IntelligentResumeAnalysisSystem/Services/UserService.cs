using Backend_IntelligentResumeAnalysisSystem.Data.Repository;
using Backend_IntelligentResumeAnalysisSystem.Dtos;
using Backend_IntelligentResumeAnalysisSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_IntelligentResumeAnalysisSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;

        public UserService(IUserRepository specificRepository, ICompanyRepository companyRepository)
        {
            _userRepository = specificRepository;
            _companyRepository = companyRepository;
        }

        public LoginModelClass IsLogin(string account, string password)
        {
            var user = _userRepository.IsLogin(account, password);

            var model = new LoginModelClass();
            if (user != null)
            {
                model.Code = 20000;
                model.UserId = user.ID;
                model.Data = user.Role;
            }
            else
            {
                model.Code = 60204;
            }
            return model;
        }

        public RegisterModelClass CreateNewAccount(RegisterSentModel register)
        {
            var company = _companyRepository.GetCompanyByName(register.Name);
            var user = _userRepository.GetUserByName(register.Account);
            var model = new RegisterModelClass();   

            if (company != null)
            {
                // 公司已经存在
                model.IsSuccess = false;
                model.Msg = "Company already exists.";
            }
            else if (user != null)
            {
                // 账号已经存在
                model.IsSuccess = false;
                model.Msg = "Account already exists.";
            }
            else
            {
                // 添加新的公司
                company = new Company
                {
                    Name = register.Name,
                    Email = register.Email,
                };
                _companyRepository.AddCompany(company);


                user = new User
                {
                    CompanyID = company.ID,
                    Account = register.Account,
                    Password = register.Password,
                    Role = "admin",
                    Company = company,
                };
                _userRepository.AddUser(user);
                model.IsSuccess = true;
                model.Msg = "Registration successful.";
            }

            return model;
        }

    }
}
