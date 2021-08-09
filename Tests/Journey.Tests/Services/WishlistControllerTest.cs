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

    public class WishlistControllerTest
    {
        [Fact]
        public async Task AddingGamesToWishlistShouldWorkCorrectly()
        {
            var list = new List<Wishlist>();

            var gameRepo = new Mock<IDeletableEntityRepository<Game>>();
            var wishRepo = new Mock<IDeletableEntityRepository<Wishlist>>();

            wishRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            wishRepo.Setup(x => x.AddAsync(It.IsAny<Wishlist>())).Callback(
                (Wishlist wish) => list.Add(wish));
            var service = new WishlistService(wishRepo.Object, gameRepo.Object);

            var userId = Guid.NewGuid().ToString();

            await service.AddToWishlist(userId, 1);
            await service.AddToWishlist(userId, 2);
            await service.AddToWishlist(userId, 3);
            await service.AddToWishlist(userId, 2);
            await service.AddToWishlist(userId, 1);

            Assert.Equal(3, list.Count());
        }
    }
}
