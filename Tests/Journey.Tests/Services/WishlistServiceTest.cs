namespace Journey.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Moq;
    using Xunit;

    public class WishlistServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Wishlist>> wishRepo;
        private readonly Mock<IDeletableEntityRepository<Game>> gameRepo;
        private readonly List<Wishlist> list;
        private readonly WishlistService service;

        public WishlistServiceTest()
        {
            this.wishRepo = new Mock<IDeletableEntityRepository<Wishlist>>();
            this.gameRepo = new Mock<IDeletableEntityRepository<Game>>();

            this.list = new List<Wishlist>();
            this.service = new WishlistService(this.wishRepo.Object, this.gameRepo.Object);

            this.wishRepo.Setup(x => x.All()).Returns(this.list.AsQueryable());
            this.wishRepo.Setup(x => x.AddAsync(It.IsAny<Wishlist>())).Callback(
                (Wishlist wish) => this.list.Add(wish));
            this.wishRepo.Setup(x => x.Delete(It.IsAny<Wishlist>())).Callback(
                (Wishlist wish) => this.list.Remove(wish));
            this.wishRepo.Setup(x => x.HardDelete(It.IsAny<Wishlist>())).Callback(
                (Wishlist wish) => this.list.Remove(wish));
            var service = new WishlistService(this.wishRepo.Object, this.gameRepo.Object);
        }

        [Fact]
        public async Task AddingGamesToWishlistShouldWorkCorrectly()
        {
            var userId = Guid.NewGuid().ToString();

            await this.service.AddToWishlist(userId, 1);
            await this.service.AddToWishlist(userId, 2);
            await this.service.AddToWishlist(userId, 3);
            await this.service.AddToWishlist(userId, 2);
            await this.service.AddToWishlist(userId, 1);

            Assert.Equal(3, this.list.Count());
        }

        [Fact]
        public async Task RemovingGamesFromWishlistShouldWorkCorrectly()
        {
            var userId = Guid.NewGuid().ToString();

            await this.service.AddToWishlist(userId, 1);
            await this.service.AddToWishlist(userId, 2);
            await this.service.AddToWishlist(userId, 3);
            await this.service.AddToWishlist(userId, 4);
            await this.service.AddToWishlist(userId, 5);

            await this.service.RemoveFromWishlist(userId, 1);

            Assert.Equal(4, this.list.Count());
        }

        [Fact]
        public async Task IsInWishShouldWorkCorrectly()
        {
            var userId = Guid.NewGuid().ToString();

            await this.service.AddToWishlist(userId, 1);
            await this.service.AddToWishlist(userId, 2);
            await this.service.AddToWishlist(userId, 3);
            await this.service.AddToWishlist(userId, 2);
            await this.service.AddToWishlist(userId, 1);

            await this.service.RemoveFromWishlist(userId, 1);

            Assert.True(this.service.IsInWish(userId, 3));
        }
    }
}
