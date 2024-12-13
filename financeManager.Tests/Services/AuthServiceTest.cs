using AutoFixture;
using FinanceManager.Application.Services;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Repositories.@interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Services
{
    public class AuthServiceTest
    {
        private readonly Mock<IUserRepositories> repository;
        private readonly Fixture fixture;

        public AuthServiceTest()
        {
            this.repository = new Mock<IUserRepositories>();
            this.fixture = new Fixture();
        }
        public async Task ShouldBeReturnInvalidEmailInDatabase()
        {
            var InputNotExistEmail = fixture.Create<Login>();

            repository.Setup(i => i.GetUserByEmail(InputNotExistEmail.Email)).ReturnsAsync((User?) null);

            var authService = new AuthService(repository.Object);

            var errorEmailIsNotFound = Assert.ThrowsAsync <Exception>(async () =>
            {
                throw new Exception("something is wrong");
            });
        }

    }
}
