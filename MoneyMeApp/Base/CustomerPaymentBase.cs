using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMeApp.Base
{
    public abstract class CustomerPaymentBase
    {
        [Required]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Amount { get; set; }
        [Range(1, 120, ErrorMessage = "Duration out of range. Range should be between 1 to 120 months.")]
        public int Duration { get; set; }
    }
}
