using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMeWebApplication.Objects
{
    public class CustomerPaymentDetails
    {
        public int ProductId { get; set; }
        public int CustomerPaymentId { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
    }
}
