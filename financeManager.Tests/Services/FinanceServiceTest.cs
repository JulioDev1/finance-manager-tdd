using AutoFixture;
using AutoMapper;
using FinanceManager.Application.Mapping;
using FinanceManager.Application.Services;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Repositories;
using FinanceManager.Infrastructure.Repositories.@interface;
using FinanceManager.Shared.Dtos;
using Moq;
using System.ComponentModel;

namespace FinanceManager.Tests.Services
{
    public class FinanceServiceTest
    {
        private readonly Mock<IFinanceRepository> repository;
        private readonly Fixture fixture;
        public FinanceServiceTest()
        {
            this.repository = new Mock<IFinanceRepository>();
            this.fixture = new Fixture();        
        }

        [Fact]
        public async Task ShouldBeReturnInvalidFinanceType()
        {
            var InputWithInvalidFinanceType =
                fixture.Build<Finance>()
                .With(f => f.Type ,(TypeFinance)999)
                .Create();

            var InputWithInvalidServiceFinance = 
                fixture.Build<CreateFinanceDto>()
                .With(f=> f.Type, (FinanceType)999)
                .Create();

            repository.Setup( i =>  i.CreateUserFinance(It.IsAny<Finance>())).ThrowsAsync(new InvalidEnumArgumentException("type finance not exists"));

            var service = new FinanceService(repository.Object);

            var exception = await Assert.ThrowsAsync<InvalidEnumArgumentException>(() => service.CreateUserFinance(InputWithInvalidServiceFinance));

            Assert.Equal("type finance not exists", exception.Message);
        }
        [Fact]

        public async Task ShouldBeReturnSuccessInCaseAllDataAreCorrectly()
        {
            var InputWithValidInput = fixture.Build<Finance>()
                .With(f => f.Type, (TypeFinance)1)
                .With(f => f.Id, 0)
                .Create();


            var InputWithValidServiceFinance = fixture.Build<CreateFinanceDto>()
                .With(f => f.Type, (FinanceType)InputWithValidInput.Type)
                .With(f => f.Status, InputWithValidInput.Status)
                .With(f => f.Name, InputWithValidInput.Name)
                .With(f => f.UserId, InputWithValidInput.UserId)
                .With(f => f.Quantity, InputWithValidInput.Quantity)
                .Create();

            repository.Setup(i => i.CreateUserFinance(InputWithValidInput)).ReturnsAsync(InputWithValidInput.Id);

            var service = new FinanceService(repository.Object);
            
            var SuccessCreateFinance = await service.CreateUserFinance(InputWithValidServiceFinance);

            Console.WriteLine(SuccessCreateFinance + " " +  InputWithValidInput);

            Assert.Equal(InputWithValidInput.Id, SuccessCreateFinance);
        }

    }
}
