using MoneyMeWebApplication.Objects;

namespace MoneyMeWebApplication.ViewModel
{
    public class CustomerSummaryDetailsViewModel
    {
        public Customer Customer { get; set; }
        public CustomerPayment CustomerPayment { get; set; }
        public CustomerPaymentProduct CustomerPaymentProduct { get; set; }
    }
}
