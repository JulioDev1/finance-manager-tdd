using FinanceManager.Domain.Interfaces;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepositories userRepositories;

        public UserService(IUserRepositories userRepositories)
        {
            this.userRepositories = userRepositories;
        }

        public async Task<User?> CreateUserService(User user)
        {
            var verifyEmailAlreadyRegister = await userRepositories.FindUserByEmail(user.Email);

            if (verifyEmailAlreadyRegister is true)
            {
                throw new Exception("email already used");
            }

            return await userRepositories.CreateUserToDatabase(user);
        }
    }
}
