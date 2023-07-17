using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PaymentGateway.Common;
using PaymentGateway.DB;
using PaymentGateway.DbModel;
using PaymentGateway.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static PaymentGateway.Common.CommonEnum;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PaymentContext _db;
        public UserController(PaymentContext database)
        {
            _db = database;
        }
        [HttpPost]
        public async Task<ApiResponse> AddUser(UserModel user)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    var cardData = _db.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                    if (cardData == null)
                    {
                        User model = new User()
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            PhoneNumber = user.Phone,
                            Password = user.Password,
                            CreatedAt = DateTime.UtcNow,
                            IsActive = true,

                        };
                        await _db.Users.AddAsync(model);
                        if (await _db.SaveChangesAsync() > 0)
                        {
                            response.Code = (int)ResponseEnum.success;
                            response.Message = "User Added Successfully";
                        }
                        else
                        {
                            response.Code = (int)ResponseEnum.bad_request;
                            response.Message = "Something went wrong!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Code = (int)ResponseEnum.internal_server_error;
                response.Message = ex.Message;

            }
            return response;
        }

        //[HttpPost]
        //public IActionResult Login([FromBody] LoginModel user)
        //{
        //    try
        //    {
        //        var userdata = _db.Users.Where(x => x.Email == user.Email).FirstOrDefault();
        //        if (userdata.Email.ToLower().Trim() == user.Email.ToLower().Trim() && userdata.Password == user.Password)
        //        {
        //            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManagers.AppSetting["JWT:Secret"]));
        //            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        //            var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManagers.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManagers.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
        //            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        //            return Ok(new UserTokens
        //            {
        //                Token = tokenString
        //            });
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return Unauthorized();

        //}

    }
}
