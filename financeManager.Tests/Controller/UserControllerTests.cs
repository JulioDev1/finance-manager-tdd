using AutoFixture;
using FinanceManager.API.Controllers;
using FinanceManager.Application.Dto_s;
using FinanceManager.Domain.Interfaces;
using FinanceManager.Domain.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Controller
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> userServiceMock;
        private readonly Fixture fixture;

        public UserControllerTests()
        {
            this.userServiceMock = new Mock<IUserService>();
            this.fixture = new Fixture();
        }
        [Fact]
        public async Task ShouldBeReturnExceptionCaseEmailAlreadyExists()
        {
            var InputUserRegister = fixture.Create<RegisterUserDto>();

            userServiceMock.Setup(u => u.FindUserExists(InputUserRegister.Email)).ReturnsAsync(true);

            var userController = new UserController(userServiceMock.Object);
            
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await userController.RegisterUser(InputUserRegister);
            
            });

            Assert.Equal("email already registred", exception.Message);        

            userServiceMock.Verify(s => s.FindUserExists(InputUserRegister.Email), Times.Once);
        }
    }
}
