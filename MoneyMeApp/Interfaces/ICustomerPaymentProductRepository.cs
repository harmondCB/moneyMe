using MoneyMeApp.Models;
using System.Collections.Generic;

namespace MoneyMeApp.Interfaces
{
    public interface ICustomerPaymentProductRepository
    {
        CustomerPaymentProduct GetCustomerPaymentProduct(int customersPaymentProductId);
        CustomerPaymentProduct GetCustomerPaymentProductByPaymentId(int customersPaymentId);
        DTO.CustomerPaymentProduct GetCustomerPaymentProductFullDetails(int customerPaymentProductId);
        CustomerPaymentProduct AddCustomerPaymentProduct(CustomerPaymentProduct customersPaymentProduct);
        CustomerPaymentProduct UpdateCustomerPaymentProduct(CustomerPaymentProduct customersPaymentProduct);
        CustomerPaymentProduct DeleteCustomerPaymentProduct(int customersPaymentProductId);
        ICollection<CustomerPaymentProduct> GetCustomerPaymentProducts();
    }
}
