using Business.HelperModels;
using Business.ServicesInterface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var result = await accountService.LoginAsync(user);

            if (string.IsNullOrEmpty(result))

                return Unauthorized();

            return Ok(result);
        }
    }
}
