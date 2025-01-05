using AutoFixture;
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

    }
}
