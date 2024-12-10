using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Model;

namespace FinanceManager.Infrastructure.Repositories
{
    public interface IUserRepositories
    {
        Task<User?> CreateUserToDatabase( User user);
        Task<bool> FindUserByEmail(string email);
    }
}
