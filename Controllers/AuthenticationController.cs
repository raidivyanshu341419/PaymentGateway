using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PaymentGateway.Common;
using PaymentGateway.DB;
using PaymentGateway.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly PaymentContext _db;

        public AuthenticationController(PaymentContext database)
        {
            _db = database;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel user)
        {
            try
            {
                var userdata = _db.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (userdata.Email.ToLower().Trim() == user.Email.ToLower().Trim() && userdata.Password == user.Password)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManagers.AppSetting["JsonWebTokenKeys:Secret"]));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManagers.AppSetting["JsonWebTokenKeys:ValidIssuer"], audience: ConfigurationManagers.AppSetting["JsonWebTokenKeys:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new UserTokens
                    {
                        Token = tokenString
                    });
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Unauthorized();

        }
    }
}
