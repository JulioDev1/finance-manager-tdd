using FinanceManager.Domain.Interfaces;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Repositories.@interface;
using FinanceManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Services
{
    public class FinanceService : IFinanceService
    {
        private IFinanceRepository financeRepository;

        public FinanceService(IFinanceRepository financeRepository)
        {
            this.financeRepository = financeRepository;
        }

        public async Task<int> CreateUserFinance(CreateFinanceDto createFinanceDto)
        {
            var validTypes = new HashSet<FinanceType> { FinanceType.Profit, FinanceType.Debt };
           
            if(!validTypes.Contains(createFinanceDto.Type))
            {
                throw new InvalidEnumArgumentException("type finance not exists");
            }

            var finance = new Finance
            {
                Name = createFinanceDto.Name,
                Status = createFinanceDto.Status,
                Quantity = createFinanceDto.Quantity,
                Type = (TypeFinance)createFinanceDto.Type,
                UserId =  createFinanceDto.UserId,
            };

            var createUserFinance = await financeRepository.CreateUserFinance(finance);
            
            return createUserFinance;
        }

        public async Task<List<Finance>> ListAllUserFinance(int id)
        {
            return await financeRepository.ListAllUserFinance(id);
        }
    }
}
