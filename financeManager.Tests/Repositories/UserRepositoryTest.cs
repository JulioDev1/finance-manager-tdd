using AutoFixture;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Repositories
{
    public class UserRepositoryTest
    {
        private readonly AppDbContext context;
        private readonly Fixture fixture;

        public UserRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "base-test")
                .Options;

            context = new AppDbContext(options);
            this.fixture = new Fixture();
        }

        [Fact]
        public void ShouldBeReturnSuccessAddUserDatabase() 
        {
            var userCreated = fixture.Create<User>();

            context.Add<User>(userCreated);
            
            context.SaveChanges();

            Assert.Single(context.Users);
        }
        [Fact]
        public async Task ShouldBeReturnTrueCaseFindUser() 
        {
            var userCreated = fixture.Create<User>();

            context.Users.Add(userCreated);

            await context.SaveChangesAsync();

            var isMatch = context.Users.Any(u => u.Email == userCreated.Email);

            Assert.True(isMatch);
        }
        [Fact]
        public void ShouldBeReturnFalseWhenFindEmailDatabase()
        {
            var falseEmail = "false@mail.com";

            var isNotMatch = context.Users.Any(u => u.Email == falseEmail);

            Assert.False(isNotMatch);
        }

        [Fact]
        public async Task FindUserInDatabaseAndReturnYourEmail()
        {
            var userCreated = fixture.Create<User>();

            context.Users.Add(userCreated);

            await context.SaveChangesAsync();

            var foundEmail = await context.Users.FirstOrDefaultAsync<User>(u=> u.Email == userCreated.Email);

            Assert.Equal(userCreated.Email, foundEmail.Email);
        }

    };
}
