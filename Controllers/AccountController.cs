using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Model;
using PaymentGateway.Services.Interface;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountService;

        public AccountController(IAccount accountService)
        {
            _accountService = accountService;
        }

        //POST api/Registration
        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var data = await _accountService.Registration(model);
            return Ok(data);
        }


        // POST api/Login
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return Ok(await _accountService.LoginUser(loginModel));
        }
    }
}
