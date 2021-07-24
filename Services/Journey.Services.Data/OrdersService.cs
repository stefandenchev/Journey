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
    using Journey.Web.ViewModels.Cart;

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
            return this.ordersRepository.All().To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var order = this.ordersRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return order;
        }

        public T GetLatest<T>(string userId)
        {
            var order = this.ordersRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(o => o.PurchaseDate)
                .To<T>()
                .FirstOrDefault();

            return order;
        }

        public IEnumerable<T> GetAllOrderItems<T>()
        {
            return this.orderItemsRepository.All().To<T>().ToList();
        }

        public bool IsInLibrary(string userId, int gameId)
        {
            List<string> allOrderIds = new();
            var allOrders = this.ordersRepository.All().Where(o => o.UserId == userId);
            foreach (Order o in allOrders)
            {
                allOrderIds.Add(o.Id);
            }

            var isBought = this.orderItemsRepository.All().Any(x => x.GameId == gameId && allOrderIds.Contains(x.OrderId));
            return isBought;
        }

        public IEnumerable<int> GetGameIdsFromOrder(string orderId)
        {
            var orderItems = this.orderItemsRepository.AllAsNoTracking().Where(oi => oi.OrderId == orderId).ToList();
            List<int> gameIds = new();

            foreach (var oi in orderItems)
            {
                gameIds.Add(oi.GameId);
            }

            return gameIds;
        }

        public IEnumerable<T> GetOrderItems<T>(string orderId)
        {
            var orderItems = this.orderItemsRepository.AllAsNoTracking().Where(oi => oi.OrderId == orderId).To<T>().ToList();
            return orderItems;
        }

        public async Task CreateOrderItems(IEnumerable<GameInCartViewModel> games, string orderId)
        {
            foreach (var game in games)
            {
                await this.orderItemsRepository.AddAsync(new OrderItem
                {
                    OrderId = orderId,
                    GameId = game.Id,
                    GameKey = RandomKeyGen(),
                    PriceOnPurchase = game.CurrentPrice,
                });
            }
        }

        private static string RandomKeyGen()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new List<char>();
            var random = new Random();

            for (int i = 0; i < 16; i++)
            {
                if (i % 4 == 0 && i != 0)
                {
                    stringChars.Add('-');
                }

                stringChars.Add(chars[random.Next(chars.Length)]);
            }

            var finalString = new string(stringChars.ToArray());
            return finalString;
        }
    }
}
