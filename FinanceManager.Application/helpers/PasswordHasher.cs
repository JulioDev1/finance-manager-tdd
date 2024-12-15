using FinanceManager.Domain.Interfaces;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.helpers
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
           return  BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Compare(string password, string comparePassword)
        {
           return BCrypt.Net.BCrypt.Verify(password, comparePassword); 
        }
    }
}
