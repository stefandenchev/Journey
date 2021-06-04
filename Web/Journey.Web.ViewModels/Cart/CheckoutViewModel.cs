namespace Journey.Web.ViewModels.Cart
{
    using System.Collections.Generic;

    using Journey.Data.Models;

    public class CheckoutViewModel : CartViewModel
    {
        public int PaymentMethodId { get; set; }

        public string CreditCardLast4 { get; set; }

        public IEnumerable<CreditCard> CreditCards { get; set; }
    }
}
