using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMeWebApplication.Objects
{
    public class CustomerDetailsResult : CustomerDetails
    {
        public int CustomerPaymentId { get; set; }
    }
}
