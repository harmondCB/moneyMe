using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMeWebApplication.Objects
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal InterestRate { get; set; }
        public int InterestFreeMonth { get; set; }
    }
}
