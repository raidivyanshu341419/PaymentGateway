using PaymentGateway.Model.DbModel;

namespace PaymentGateway.Model
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        public decimal Amount { get; set; }
    }
}
