using AutoFixture;
using FinanceManager.Application.Services;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Repositories.@interface;
using Moq;
using static BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Tests.Services
{
    public class AuthServiceTest
    {
        private readonly Mock<IUserRepositories> repository;
        private readonly Mock<IPasswordHasher> passwordHashed;
        private readonly Fixture fixture;

        public AuthServiceTest()
        {
            this.repository = new Mock<IUserRepositories>();
            this.fixture = new Fixture();
            this.passwordHashed = new Mock<IPasswordHasher>();
        }
        [Fact]
        public async Task ShouldBeReturnInvalidEmailInDatabase()
        {
            var InputNotExistEmail = fixture.Create<Login>();

            repository.Setup(i => i.GetUserByEmail(InputNotExistEmail.Email)).ReturnsAsync((User?) null);

            var authService = new AuthService(repository.Object);

            var errorEmailIsNotFound = await Assert.ThrowsAsync <Exception>(async () =>
            {
                await authService.GenerateAuthToken(InputNotExistEmail);
            });
            Assert.Equal("something is wrong", errorEmailIsNotFound.Message);
        }
        [Fact]
        public void ShouldBeReturnErrorPassword()
        {
            var userWithPasswordIncorrectly = "another password";
            
            var userModelWithIncorrectlyPassword = "incorrectly password";

            passwordHashed.Setup(u => u.Compare(userWithPasswordIncorrectly, userModelWithIncorrectlyPassword));

            bool result = passwordHashed.Object.Compare(userWithPasswordIncorrectly, userModelWithIncorrectlyPassword);

            Assert.False(result);
        }
        [Fact]
        public void ShouldBeReturnSuccesPassword()
        {
            var userWithPasswordCorrect = "correct password";

            var correctToComparePassword = "correct password";

            passwordHashed.Setup(u => u.Compare(userWithPasswordCorrect, correctToComparePassword)).Returns(true);

            bool result = passwordHashed.Object.Compare(userWithPasswordCorrect, correctToComparePassword);

            Assert.True(result);
        }

    }
}
