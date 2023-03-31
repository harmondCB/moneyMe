using MoneyMeApp.Base;
using System.Collections.Generic;

namespace MoneyMeApp.Models
{
    public class CustomerPayment : CustomerPaymentBase
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public CustomerPaymentProduct CustomerPaymentProduct { get; set; }
    }
}
