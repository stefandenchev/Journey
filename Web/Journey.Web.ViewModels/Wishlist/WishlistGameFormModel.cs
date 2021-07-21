namespace Journey.Web.ViewModels.Wishlist
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class WishlistGameFormModel : IMapFrom<Wishlist>
    {
        public string UserId { get; set; }

        public int GameId { get; set; }
    }
}
