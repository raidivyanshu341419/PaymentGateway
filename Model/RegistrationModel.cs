namespace PaymentGateway.Model
{
    public class RegistrationModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string RegistrationNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int? Role { get; set; }
    }
    public class Foretpassword
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
