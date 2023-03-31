using MoneyMeApp.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMeApp.DTO
{
    public class CustomerDetails : CustomerBase
    {
        [Range(1, 120, ErrorMessage = "Duration out of range. Range should be between 1 to 120 months.")]
        public int Term { get; set; }
        [DataType(DataType.Date)]

        [Column(TypeName = "decimal(19, 4)")]
        public decimal AmountRequired { get; set; }
    }
}
