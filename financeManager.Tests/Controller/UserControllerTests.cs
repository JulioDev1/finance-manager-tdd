using AutoFixture;
using FinanceManager.API.Controllers;
using FinanceManager.Application.Dto_s;
using FinanceManager.Domain.Interfaces;
using FinanceManager.Domain.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        [Fact]
        public async Task ShouldBeReturnSuccesInCaseResultAreCorrect()
        {
            var inputUserRegister = fixture.Create<RegisterUserDto>();
            
            
            var userDependecieInjection = new User
            {
                Email = inputUserRegister.Email,
                Name = inputUserRegister.Name,
                Password = inputUserRegister.Password
            };

            userServiceMock.Setup(u=> u.FindUserExists(inputUserRegister.Email)).ReturnsAsync(false);

            userServiceMock.Setup(u => u.CreateUserService(It.IsAny<User>())).ReturnsAsync(userDependecieInjection);
            
            var userController = new UserController(userServiceMock.Object);

            var successControllerUser = await userController.RegisterUser(inputUserRegister);

            var okResult = Assert.IsType<OkObjectResult>(successControllerUser.Result);
        }
    }
}
