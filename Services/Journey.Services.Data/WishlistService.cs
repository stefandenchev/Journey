namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class WishlistService : IWishlistService
    {
        private readonly IDeletableEntityRepository<Wishlist> wishListRepository;
        private readonly IDeletableEntityRepository<Game> gamesRepository;

        public WishlistService(
            IDeletableEntityRepository<Wishlist> wishListRepository,
            IDeletableEntityRepository<Game> gamesRepository)
        {
            this.wishListRepository = wishListRepository;
            this.gamesRepository = gamesRepository;
        }

        public T GetById<T>(string userId, int gameId)
        {
            var wishListItem = this.wishListRepository
                .All()
                .Where(x => x.UserId == userId && x.GameId == gameId)
                .To<T>()
                .FirstOrDefault();

            return wishListItem;
        }

        public async Task AddToWishlist(string userId, int gameId)
        {
            Wishlist wish = new Wishlist() { UserId = userId, GameId = gameId };
            await this.wishListRepository.AddAsync(wish);
            await this.wishListRepository.SaveChangesAsync();
        }

        public async Task RemoveFromWishlist(string userId, int gameId)
        {
            Wishlist wish = this.wishListRepository.All().FirstOrDefault(c => c.UserId == userId && c.GameId == gameId);
            this.wishListRepository.Delete(wish);
            await this.wishListRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllForUser<T>(string userId)
        {
            var wishlist = this.wishListRepository
                .All()
                .Where(x => x.UserId == userId)
                .ToList();

            List<int> ids = new();
            foreach (var game in wishlist)
            {
                ids.Add(game.GameId);
            }

            var games = this.gamesRepository.All().Where(x => ids.Contains(x.Id)).To<T>();

            return games;
        }

        public bool IsInWish(string userId, int gameId)
        {
            var isWished = this.wishListRepository.All().Any(x => x.UserId == userId && x.GameId == gameId);
            return isWished;
        }
    }
}
