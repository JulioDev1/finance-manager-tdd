﻿using FinanceManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateAuthToken(Login login);
    }
}