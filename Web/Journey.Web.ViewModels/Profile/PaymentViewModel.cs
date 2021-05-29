namespace Journey.Web.ViewModels.Profile
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Models;

    public class PaymentViewModel
    {
        [Display(Name = "Your credit cards")]
        public List<CreditCard> CreditCards { get; set; }
    }
}
