using MoneyMeApp.Models;
using System.Collections.Generic;

namespace MoneyMeApp.Interfaces
{
    public interface ICustomerPaymentRepository
    {
        CustomerPayment GetCustomerPayment(int paymentId);
        CustomerPayment AddCustomerPayment(CustomerPayment customerPayment);
        CustomerPayment UpdateCustomerPayment(CustomerPayment customerPayment);
        ICollection<CustomerPayment> GetCustomerPayments();
    }
}
