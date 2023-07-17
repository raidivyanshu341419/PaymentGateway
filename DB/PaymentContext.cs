using Microsoft.EntityFrameworkCore;
using PaymentGateway.DbModel;

namespace PaymentGateway.DB
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseSerialColumns();
        }
        public DbSet<Payment> Payments { get; set; }    
        public DbSet<User> Users { get; set; }    
        
    }
}
