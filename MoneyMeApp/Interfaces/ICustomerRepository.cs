using MoneyMeApp.Models;
using System.Collections.Generic;

namespace MoneyMeApp.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetCustomers(int customerId);
        Customer AddCustomer(Customer customers);
        Customer UpdateCustomer(Customer customers);
        Customer DeleteCustomers(int customerId);
        ICollection<Customer> GetCustomers();
    }
}
