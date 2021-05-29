namespace Journey.Web.ViewModels.Cart
{
    using Journey.Data.Models;
    using System.Collections.Generic;

    public class CheckoutViewModel : CartViewModel
    {
        public string PaymentMethodId { get; set; }

        public string CreditCardLast4 { get; set; }

        public List<CreditCard> CreditCards { get; set; }
    }
}
