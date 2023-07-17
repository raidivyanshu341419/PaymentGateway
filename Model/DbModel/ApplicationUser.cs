using Microsoft.AspNetCore.Identity;

namespace PaymentGateway.Model.DbModel
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string FullName { get; set; }
        public string? RegistrationNo { get; set; }
        public bool? PreventWebLogin { get; set; }
        public bool IsDeleted { get; set; }
        public int? Role { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
