using MoneyMeApp.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMeApp.Models
{
    public class Customer : CustomerBase
    {
        public int Id { get; set; }

        public ICollection<CustomerPayment> CustomerPayments { get; set; }
    }
}
