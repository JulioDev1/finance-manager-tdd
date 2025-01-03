using FinanceManager.Application.helpers;
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
    public class UserService : IUserService
    {
        private readonly IUserRepositories userRepositories;
        private readonly IPasswordHasher passwordHasher;


        public UserService(IUserRepositories userRepositories)
        {
            this.userRepositories = userRepositories;
            this.passwordHasher = new PasswordHasher();
        }

        public async Task<User?> CreateUserService(User user)
        {
            var verifyEmailAlreadyRegister = await userRepositories.FindUserByEmail(user.Email);

            if (verifyEmailAlreadyRegister is true)
            {
                throw new Exception("email already used");
            }
            user.Password = passwordHasher.Hash(user.Password);  

            return await userRepositories.CreateUserToDatabase(user);
        }

        public async Task<bool> FindUserExists(string email)
        {
            return await userRepositories.FindUserByEmail(email);
        }
    }
}
