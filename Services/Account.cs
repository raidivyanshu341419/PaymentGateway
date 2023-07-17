using Microsoft.AspNetCore.Identity;
using PaymentGateway.Common;
using PaymentGateway.DB;
using PaymentGateway.Model;
using PaymentGateway.Model.DbModel;
using PaymentGateway.Services.Interface;
using System.Data;
using static PaymentGateway.Common.CommonEnum;

namespace PaymentGateway.Services
{
    public class Account : IAccount
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly PaymentContext _context;
        private readonly UserManager<ApplicationUser> _user;

        public Account(PaymentContext context, UserManager<ApplicationUser> user, SignInManager<ApplicationUser> signInManager, JwtSettings jwtSettings)
        {
            _context = context;
            _user = user;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }
        public async Task<UserTokens> Registration(RegistrationModel model)
        {
            UserTokens response = new UserTokens();
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Email.ToLower().Trim() == model.Email.ToLower().Trim());
                if (user is not null)
                {
                    response.Code = (int)ResponseEnum.bad_request;
                    response.Message = "An account already exists with that email/username.";
                    return response;
                }
                var userName = model.Email.Split("@")[0].ToLower().Trim();
                ApplicationUser appUser = new ApplicationUser
                {
                    Firstname = model.Firstname.Trim().ToUpperInvariant(),
                    Lastname = model.Lastname.Trim().ToUpperInvariant(),
                    FullName = model.Firstname.Trim().ToUpperInvariant() + " " + model.Lastname.Trim().ToUpperInvariant(),
                    UserName = userName,
                    Email = model.Email,
                    NormalizedEmail = model.Email,
                    NormalizedUserName = userName,
                    ConfirmPassword = model.ConfirmPassword,
                    PhoneNumberConfirmed = false,
                    IsDeleted = false,
                };
                IdentityResult userResult = await _user.CreateAsync(appUser, model.Password);
                if (userResult.Succeeded)
                {
                    response.Code = (int)ResponseEnum.success;
                    response.Message = "Registration Completed Successfully";
                }
                else
                {
                    response.Code = (int)ResponseEnum.bad_request;
                    response.Message = "Bad Request";
                }
            }
            catch (Exception ex)   
            {
                response.Code = (int)ResponseEnum.internal_server_error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<UserTokens> LoginUser(LoginModel loginModel)
        {
            UserTokens Token = new UserTokens();
            try
            {
                var user = _user.Users.FirstOrDefault(u => u.Email == loginModel.Email && u.IsDeleted == false);
                if (user is null)
                {
                    Token.Status = "User not found";
                    Token.Code = (int)ResponseEnum.unauthorized;
                    return Token;
                }
                var userSigninResult = await _user.CheckPasswordAsync(user, loginModel.Password);
                if (userSigninResult == true /*&& user.PreventWebLogin == false*/)
                {
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        Email = user.Email.ToString(),
                        UserName = user.UserName,
                        UserId = user.Id
                    }, _jwtSettings);
                    Token.Status = "Logged In";
                    Token.FullName = user.FullName;
                    Token.Email = user.Email.ToString();
                    Token.Code = (int)ResponseEnum.success;
                    //Token.RoleType = (int)user.Role;
                    //Token.RoleId = (int)user.Role;
                    //Token.Role = _context.UserRoles.Where(x => x.role == user.Role).FirstOrDefault().Role;

                    return Token;
                }
                else
                {
                    Token.Status = "Invalid password";
                    Token.Code = (int)ResponseEnum.unauthorized;
                }
            }
            catch (Exception ex)
            {
                Token.Status = "Internal server error";
                Token.Code = (int)ResponseEnum.internal_server_error;
            }
            return Token;
        }
    }
}
