using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Compare(string password, string comparePassword);
    }
}
