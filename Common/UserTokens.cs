using static PaymentGateway.Common.CommonModel;

namespace PaymentGateway.Common
{
    public class UserTokens : ResponseModel
    {
        public string? FullName
        {
            get;
            set;
        }
        public string? Token
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public TimeSpan Validaty
        {
            get;
            set;
        }
        public string RefreshToken
        {
            get;
            set;
        }
        public string UserId
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }

        public DateTime ExpiredTime
        {
            get;
            set;
        }


        public int RoleId
        {
            get;
            set;
        }
        public int RoleType
        {
            get;
            set;
        }
        public string Role { get; set; }
        public string Status { get; set; }
        

    }
}
