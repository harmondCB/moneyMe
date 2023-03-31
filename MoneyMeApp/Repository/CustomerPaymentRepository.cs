using MoneyMeApp.Data;
using MoneyMeApp.Interfaces;
using MoneyMeApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoneyMeApp.Repository
{
    public class CustomerPaymentRepository : ICustomerPaymentRepository
    {
        private readonly DataContext context;
        public CustomerPaymentRepository(DataContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        public CustomerPayment GetCustomerPayment(int paymentId)
        {
            return this.context.CustomerPayments.FirstOrDefault(cp => cp.Id == paymentId);
        }

        public ICollection<CustomerPayment> GetCustomerPayments()
        {
            return this.context.CustomerPayments.ToList();
        }

        public CustomerPayment AddCustomerPayment(CustomerPayment customerPayment)
        {
            this.context.CustomerPayments.Add(customerPayment);
            this.context.SaveChanges();

            return customerPayment;
        }

        public CustomerPayment UpdateCustomerPayment(CustomerPayment customerPayment)
        {
            this.context.CustomerPayments.Update(customerPayment);
            this.context.SaveChanges();

            return customerPayment;
        }
    }
}
