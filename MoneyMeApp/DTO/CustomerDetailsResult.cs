using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMeApp.DTO
{
    public class CustomerDetailsResult : CustomerDetails
    {
        public int Id { get; set; }
        public int CustomerPaymentId { get; set; }
    }
}
