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

    public class OrderItemsService : IOrderItemsService
    {
        private readonly IRepository<OrderItem> orderItemsRepository;

        public OrderItemsService(IRepository<OrderItem> orderItemsRepository)
        {
            this.orderItemsRepository = orderItemsRepository;
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

        public IEnumerable<T> GetOrderItems<T>(string orderId)
        {
            var orderItems = this.orderItemsRepository.AllAsNoTracking().Where(oi => oi.OrderId == orderId).To<T>().ToList();
            return orderItems;
        }

        public IEnumerable<T> GetAllOrderItems<T>()
        {
            return this.orderItemsRepository.All().To<T>().ToList();
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
