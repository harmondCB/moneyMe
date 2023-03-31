using MoneyMeApp.Base;
using System.ComponentModel.DataAnnotations;

namespace MoneyMeApp.DTO
{
    public class CustomerPaymentDetails : CustomerPaymentBase
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerPaymentId { get; set; }
    }
}
