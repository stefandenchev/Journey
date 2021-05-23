namespace Journey.Web.ViewModels.Cart
{
    using System.Collections.Generic;

    public class CartViewModel
    {
        public List<GameInCartViewModel> GamesInCart { get; set; }

        public decimal Total { get; set; }
    }
}
