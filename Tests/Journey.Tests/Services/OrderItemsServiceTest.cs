namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Profile;
    using Moq;
    using Xunit;

    public class OrderItemsServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<OrderItem>> orderItemsRepo;
        private readonly List<OrderItem> orderItemsList;
        private readonly OrderItemsService service;

        public OrderItemsServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.orderItemsRepo = new Mock<IDeletableEntityRepository<OrderItem>>();
            this.orderItemsList = new List<OrderItem>();
            this.service = new OrderItemsService(this.orderItemsRepo.Object);

            this.orderItemsRepo.Setup(x => x.All()).Returns(this.orderItemsList.AsQueryable());
            this.orderItemsRepo.Setup(x => x.AddAsync(It.IsAny<OrderItem>())).Callback(
                (OrderItem item) => this.orderItemsList.Add(item));
            this.orderItemsRepo.Setup(x => x.Delete(It.IsAny<OrderItem>())).Callback(
                (OrderItem item) => this.orderItemsList.Remove(item));
        }

        [Fact]
        public void GetGameIdsShouldWorkCorrectly()
        {
            this.orderItemsRepo.Object.AddAsync(new OrderItem
            {
                Id = 1,
                GameId = 1,
                OrderId = "Order1",
            });

            this.orderItemsRepo.Object.AddAsync(new OrderItem
            {
                Id = 2,
                GameId = 2,
                OrderId = "Order1",
            });

            var result = this.service.GetGameIdsFromOrder("Order1");

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetOrderItemsShouldWorkCorrectly()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.orderItemsRepo.Object.AddAsync(new OrderItem
            {
                Id = 1,
                GameId = 1,
                OrderId = "Order1",
            });

            this.orderItemsRepo.Object.AddAsync(new OrderItem
            {
                Id = 2,
                GameId = 2,
                OrderId = "Order1",
            });

            var result = this.service.GetOrderItems<OrderItemViewModel>("Order1");

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
