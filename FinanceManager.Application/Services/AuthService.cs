using FinanceManager.Domain.Interfaces;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Repositories.@interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepositories userRepositories;

        public AuthService(IUserRepositories userRepositories)
        {
            this.userRepositories = userRepositories;
        }

        public async Task<string> GenerateAuthToken(Login login)
        {
            var userLogin = new Login(login.Email, login.Password);
            
            var findUserByEmail = await userRepositories.FindUserByEmail(userLogin.Email);

            if(findUserByEmail is false)
            {
                throw new Exception("user not finded");
            }


        }
    }
}
