namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Web.ViewModels.Profile;
    using Moq;
    using Xunit;

    public class OrdersServiceTest
    {
        private readonly Mock<IRepository<Order>> ordersRepo;
        private readonly Mock<IRepository<OrderItem>> orderItemsRepo;
        private readonly OrdersService service;
        private readonly List<Order> orders;

        public OrdersServiceTest()
        {
            this.ordersRepo = new Mock<IRepository<Order>>();
            this.orderItemsRepo = new Mock<IRepository<OrderItem>>();
            this.orders = new List<Order>();
            this.service = new OrdersService(this.ordersRepo.Object, this.orderItemsRepo.Object);

            this.ordersRepo.Setup(x => x.All()).Returns(this.orders.AsQueryable());

            this.ordersRepo.Setup(x => x.AddAsync(It.IsAny<Order>())).Callback(
                (Order item) => this.orders.Add(item));
            this.ordersRepo.Setup(x => x.Delete(It.IsAny<Order>())).Callback(
                (Order item) => this.orders.Remove(item));
        }

        [Fact]
        public async Task CreatingOrderWorksCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            await this.service.CreateAsync("TestOrder", user.Identity.Name, 1, 20.0m);

            Assert.Single(this.orders);
        }
    }
}
