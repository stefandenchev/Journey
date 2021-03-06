namespace Journey.Web.ViewModels.Cart
{
    using System.Collections.Generic;

    public class CheckoutViewModel : CartViewModel
    {
        public int CreditCardId { get; set; }

        public string CreditCardLast4 { get; set; }

        public IEnumerable<CreditCardViewModel> CreditCards { get; set; }
    }
}
