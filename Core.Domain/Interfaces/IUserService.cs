using FinanceManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User?> CreateUserService(User user);
        Task<bool> FindUserExists(string email);
    }
}
