using FinanceManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Infrastructure.Repositories.@interface
{
    public interface IFinanceRepository
    {
        Task<int> CreateUserFinance(Finance finance);
        Task<List<Finance>> ListAllUserFinance(int id);

    }
}
