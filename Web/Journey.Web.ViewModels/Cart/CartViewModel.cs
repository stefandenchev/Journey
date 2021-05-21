namespace Journey.Web.ViewModels.Cart
{
    using System.Collections.Generic;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class CartViewModel
    {
        public List<GameInCartViewModel> GamesInCart { get; set; }

        public decimal Total { get; set; }
    }
}
