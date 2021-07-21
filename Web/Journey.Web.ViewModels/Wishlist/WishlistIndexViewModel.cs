namespace Journey.Web.ViewModels.Wishlist
{
    using System.Collections.Generic;

    public class WishlistIndexViewModel
    {
        public IEnumerable<GameInWishlistServiceModel> Games { get; set; }
    }
}
