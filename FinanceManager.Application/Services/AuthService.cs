using FinanceManager.Domain.Interfaces;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Repositories.@interface;
using static BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using FinanceManager.Application.helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FinanceManager.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepositories userRepositories;
        private readonly IPasswordHasher passwordHasher;    

        public AuthService(IUserRepositories userRepositories, IPasswordHasher passwordHasher)
        {
            this.userRepositories = userRepositories;
            this.passwordHasher = passwordHasher;
        }

        public async Task<User?> AuthenticateUser(Login login)
        {
            var userLogin = new Login(login.Email, login.Password);
            
            var findUserByEmail = await userRepositories.GetUserByEmail(userLogin.Email);

            if(findUserByEmail is null)
            {
                throw new Exception("something is wrong");
            }

            var comparePassword = passwordHasher.Compare(userLogin.Password, findUserByEmail.Password);

            if (!comparePassword)
            {
                throw new Exception("incorrectly Password");
            }

            return findUserByEmail;
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));

            return claims;
        }

        public string GenerateAuthToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(AuthSetting.PrivateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = GenerateClaims(user)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}
