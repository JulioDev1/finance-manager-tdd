using FinanceManager.Application.Dto_s;
using FinanceManager.Domain.Interfaces;
using FinanceManager.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers
{
    [Route("user/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("/register-user")]
        public async Task<ActionResult<User?>> RegisterUser(RegisterUserDto userDto)
        {
           
            var AddUserData = new User
            {
                Email = userDto.Email,
                Name = userDto.Name,
                Password = userDto.Password,
            };
            
            var verifyEmailAlreadyRegister = await userService.FindUserExists(AddUserData.Email);
            
            if( verifyEmailAlreadyRegister)
            {
                throw new Exception("email already registred");
            }

            var user = await userService.CreateUserService(AddUserData);

            return Ok(user);
        }
    }
}
