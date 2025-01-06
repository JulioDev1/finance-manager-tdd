using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Database;
using FinanceManager.Infrastructure.Repositories.@interface;
using FinanceManager.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Infrastructure.Repositories
{
    public class FinanceRepository : IFinanceRepository
    {
        private readonly AppDbContext context;

        public FinanceRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int>  CreateUserFinance(Finance finance)
        {
            var validTypes = new HashSet<TypeFinance> { TypeFinance.Profit, TypeFinance.Debt };

            if (!validTypes.Contains(finance.Type))
            {
                throw new InvalidEnumArgumentException("type finance not exists");
            }

            context.Finances.Add(finance);

            await context.SaveChangesAsync();
            
            return finance.Id;
        }

        public async Task<List<Finance>> ListAllUserFinance(int id)
        {
            return await context.Finances.Where(f => f.User.Id == id).ToListAsync();
        }
    }
}
