namespace Journey.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> ordersRepository;
        private readonly IRepository<OrderItem> orderItemsRepository;

        public OrdersService(
            IRepository<Order> ordersRepository,
            IRepository<OrderItem> orderItemsRepository)
        {
            this.ordersRepository = ordersRepository;
            this.orderItemsRepository = orderItemsRepository;
        }

        public async Task CreateAsync(string orderId, string userId, int creditCardId, decimal total)
        {
            var order = new Order
            {
                Id = orderId,
                UserId = userId,
                PurchaseDate = DateTime.Now,
                CreditCardId = creditCardId,
                Total = total,
            };
            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.ordersRepository
                .All()
                .To<T>()
                .ToList();
        }

        public T GetById<T>(string id)
        {
            var order = this.ordersRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return order;
        }

        public T GetLatest<T>(string userId)
        {
            var order = this.ordersRepository
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(o => o.PurchaseDate)
                .To<T>()
                .FirstOrDefault();

            return order;
        }

        public bool IsInLibrary(string userId, int gameId)
        {
            List<string> allOrderIds = new();
            var allOrders = this.ordersRepository.All().Where(o => o.UserId == userId);
            foreach (Order o in allOrders)
            {
                allOrderIds.Add(o.Id);
            }

            var isBought = this.orderItemsRepository
                .All()
                .Any(x => x.GameId == gameId && allOrderIds.Contains(x.OrderId));
            return isBought;
        }

        public IEnumerable<T> GetOrders<T>(string userId)
        {
            var orders = this.ordersRepository
                .All()
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToList();

            return orders;
        }

        public IEnumerable<string> GetOrderIds(string userId)
        {
            var orderIds = this.ordersRepository
                .All()
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .ToList();

            return orderIds;
        }
    }
}
