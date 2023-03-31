using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMeWebApplication.Objects
{
    public class CustomerPaymentProduct
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Interest { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal TotalAmount { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int CustomerPaymentId { get; set; }
        public CustomerPayment CustomerPayment { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
