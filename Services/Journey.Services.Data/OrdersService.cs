﻿namespace Journey.Services.Data
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

        public OrdersService(IRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task CreateAsync(OrderViewModel input)
        {
            var order = new Order
            {
                Id = input.Id,
                UserId = input.UserId,
                PurchaseDate = DateTime.Now,
                CreditCardId = input.CreditCardId,
            };
            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.ordersRepository.All().To<T>().ToList();
        }
    }
}