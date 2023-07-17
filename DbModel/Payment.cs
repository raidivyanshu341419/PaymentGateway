using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentGateway.DbModel
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        public decimal Amount { get; set; }
        public virtual User GetUser { get; set; }
    }
}
