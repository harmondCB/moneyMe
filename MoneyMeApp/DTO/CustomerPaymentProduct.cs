using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMeApp.DTO
{
    public class CustomerPaymentProduct
    {
        public decimal Amount { get; set; }
        public int Terms { get; set; }

        [Column(TypeName = "decimal(19, 4)")]
        public decimal Interest { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal TotalAmount { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int CustomerPaymentId { get; set; }
        public string ProductName { get; set; }
    }
}
