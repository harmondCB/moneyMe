using MoneyMeWebApplication.Objects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyMeWebApplication.ViewModel
{
    public class CustomerFullDetailsViewModel
    {
        public int Term { get; set; }
        public decimal Amount { get; set; }
        public CustomerPayment CustomerPayment { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
        public int SelectedProduct { get; set; }
    }
}
