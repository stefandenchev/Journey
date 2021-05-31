namespace Journey.Data.Models
{
    using Journey.Data.Common.Models;

    public class Wishlist : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
