using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Model.DbModel;

namespace PaymentGateway.DB
{
    public class PaymentContext : IdentityDbContext<ApplicationUser>
    {
        public PaymentContext(DbContextOptions<PaymentContext> options): base(options)
        {
            
        }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseSerialColumns();
            builder.Entity<Payment>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).UseIdentityColumn();
            });
            //builder.Entity<User>(b =>
            //{
            //    b.HasKey(e => e.UserId);
            //    b.Property(e => e.UserId).UseIdentityColumn();
            //}); 
            builder.Entity<IdentityUserLogin<string>>().HasNoKey();
            builder.Entity<IdentityUserRole<string>>().HasNoKey();
            builder.Entity<IdentityUserToken<string>>().HasNoKey();
        }
    }
}
