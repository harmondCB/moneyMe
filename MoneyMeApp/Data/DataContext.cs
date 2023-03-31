using Microsoft.EntityFrameworkCore;
using MoneyMeApp.Models;

namespace MoneyMeApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerPayment> CustomerPayments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerPaymentProduct> CustomerPaymentProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 1,
                Name = "ProductA",
                InterestRate = 0,
                InterestFreeMonth = 0
            }, new Product()
            {
                Id = 2,
                Name = "ProductB",
                InterestRate = 0.05M,
                InterestFreeMonth = 2
            },
            new Product()
            {
                Id = 3,
                Name = "ProductC",
                InterestRate = 0.05M,
                InterestFreeMonth = 0
            });
        }
    }
}
