using AutoMapper;
using FinanceManager.Domain.Model;
using FinanceManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Mapping
{
    public class FinanceProfile : Profile
    {
        FinanceProfile() 
        { 
            CreateMap<Finance, CreateFinanceDto>();
            CreateMap<CreateFinanceDto,Finance>();
        }
    }
}
