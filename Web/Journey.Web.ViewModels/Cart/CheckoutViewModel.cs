namespace Journey.Web.ViewModels.Cart
{
    using Journey.Data.Models;
    using System.Collections.Generic;

    public class CheckoutViewModel : CartViewModel
    {
        public int PaymentMethodId { get; set; }

        public string CreditCardLast4 { get; set; }

        public IEnumerable<CreditCard> CreditCards { get; set; }
    }
}
