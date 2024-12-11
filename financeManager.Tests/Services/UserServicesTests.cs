using AutoFixture;
using FinanceManager.Application.Services;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace financeManager.Tests.Services
{
    public class UserServicesTests
    {
        private readonly Mock<IUserRepositories> repository;
        private readonly Fixture fixture;

        public UserServicesTests()
        {
            this.repository = new Mock<IUserRepositories>();
            this.fixture = new Fixture();
        }

        [Fact]
        public async Task ShouldBeReturnErrorExistsEmail()
        {
            var InputUserRegister = fixture.Create<User>();

            repository.Setup(r => r.FindUserByEmail(InputUserRegister.Email)).ReturnsAsync(true);

            var userService = new UserService(repository.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await userService.CreateUserService(InputUserRegister);
            });

            Assert.Equal("email already used", exception.Message);

            repository.Verify(r => r.FindUserByEmail(InputUserRegister.Email), Times.Once);

            repository.Verify(r => r.CreateUserToDatabase(InputUserRegister), Times.Never);
        }

        [Fact]
        public async Task ShouldBeReturnSucessWithUserData()
        {
            var InputUserRegisterSuccess = fixture.Create<User>();

            repository.Setup(r => r.FindUserByEmail(InputUserRegisterSuccess.Email)).ReturnsAsync(false);

            repository.Setup(r => r.CreateUserToDatabase(InputUserRegisterSuccess)).ReturnsAsync(InputUserRegisterSuccess);

            var userService = new UserService(repository.Object);

            var userServiceReturnSuccess = await userService.CreateUserService(InputUserRegisterSuccess);

            Assert.Equal(userServiceReturnSuccess, InputUserRegisterSuccess);

            repository.Verify(r => r.FindUserByEmail(InputUserRegisterSuccess.Email), Times.Once);

            repository.Verify(r=> r.CreateUserToDatabase(InputUserRegisterSuccess), Times.Once);
        }

    }
}
