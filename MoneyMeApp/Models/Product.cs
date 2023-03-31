using MoneyMeApp.Base;
using System.Collections.Generic;

namespace MoneyMeApp.Models
{
    public class Product : ProductBase
    {
        public int Id { get; set; }

        public ICollection<CustomerPaymentProduct> CustomerPaymentProduct { get; set; }
    }
}
