using FinanceManager.Domain.Model;
using FinanceManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Interfaces
{
    public interface IFinanceService
    {
        Task<int> CreateUserFinance(CreateFinanceDto createFinanceDto);
        Task<List<Finance>> ListAllUserFinance(int id);

    }
}
