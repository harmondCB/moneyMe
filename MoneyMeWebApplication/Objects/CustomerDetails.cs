using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMeWebApplication.Objects
{
    public class CustomerDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Title { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public decimal AmountRequired { get; set; }
        public int Term { get; set; }
    }
}
