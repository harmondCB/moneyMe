using MoneyMeApp.Data;
using MoneyMeApp.Interfaces;
using MoneyMeApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoneyMeApp.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext context;

        public CustomerRepository(DataContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        public ICollection<Customer> GetCustomers()
        {
            return this.context.Customers.OrderBy(c => c.FirstName).ToList();
        }


        public Customer GetCustomers(int customerId)
        {
            return this.context.Customers.FirstOrDefault(c => c.Id == customerId);
        }

        public Customer AddCustomer(Customer customers)
        {
            this.context.Customers.Add(customers);
            this.context.SaveChanges();

            return customers;
        }

        public Customer UpdateCustomer(Customer customers)
        {
            this.context.Customers.Update(customers);
            this.context.SaveChanges();

            return customers;
        }

        public Customer DeleteCustomers(int customerId)
        {
            var result = this.context.Customers.FirstOrDefault(x => x.Id == customerId);

            if (result != null)
            {
                this.context.Customers.Remove(result);
                this.context.SaveChanges();
            }

            return result;
        }
    }
}
