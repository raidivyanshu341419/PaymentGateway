using PaymentGateway.Common;
using PaymentGateway.Model;

namespace PaymentGateway.Services.Interface
{
    public interface IAccount
    {
        Task<UserTokens> Registration(RegistrationModel model);
        Task<UserTokens> LoginUser(LoginModel loginModel);
    }
}