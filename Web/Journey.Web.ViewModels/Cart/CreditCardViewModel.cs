using Journey.Data.Models;
using Journey.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Journey.Web.ViewModels.Cart
{
    public class CreditCardViewModel : IMapFrom<CreditCard>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }

        public string CVV { get; set; }
    }
}
