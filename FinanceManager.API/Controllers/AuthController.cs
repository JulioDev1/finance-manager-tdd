using FinanceManager.Domain.Interfaces;
using FinanceManager.Domain.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers
{
        [Route("auth/[controller]")]
        [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("/auth")]
        public async Task<ActionResult> AuthenticateController([FromBody] Login login)
        {
            try
            {
                var auth = await authService.AuthenticateUser(login);

                var generateToken =  authService.GenerateAuthToken(auth!);

                return Ok(generateToken);

            } catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
    }
}
