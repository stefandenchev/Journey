namespace Journey.Web.ViewModels.Cart
{
    using System.Collections.Generic;

    public class CartViewModel
    {
        public string Id { get; set; }

        public List<GameInCartViewModel> GamesInCart { get; set; }

        public decimal Total { get; set; }
    }
}
