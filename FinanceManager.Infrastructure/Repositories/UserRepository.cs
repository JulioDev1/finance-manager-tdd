using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Database;
using FinanceManager.Infrastructure.Repositories.@interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Infrastructure.Repositories
{
    public class UserRepository : IUserRepositories
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<User?> CreateUserToDatabase(User user)
        {
            context.Users.Add(user);
            
            await context.SaveChangesAsync();
            
            return user;
        }

        public async Task<bool> FindUserByEmail(string email)
        {
          return await context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
