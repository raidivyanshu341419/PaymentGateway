namespace PaymentGateway.Model
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
