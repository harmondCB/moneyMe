using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMeApp.Base
{
    public abstract class ProductBase
    {
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "decimal(19, 4)")]
        public decimal InterestRate { get; set; }
        public int InterestFreeMonth { get; set; }
    }
}
