namespace Journey.Web.ViewModels.Profile
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Models;
    using Journey.Web.ViewModels.Cart;

    public class PaymentViewModel
    {
        [Display(Name = "Your credit cards")]
        public IEnumerable<CreditCardViewModel> CreditCards { get; set; }
    }
}
