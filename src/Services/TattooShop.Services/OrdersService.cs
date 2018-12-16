using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> _ordersRepository;

        public OrdersService(IRepository<Order> ordersRepository)
        {
            this._ordersRepository = ordersRepository;
        }

        public async Task<bool> AddOrder(string address, string description, int quantity, string productId, string userId, string userAddress)
        {
            if (description == null ||
                productId == null ||
                userId == null)
            {
                return false;
            }

            string deliveryAddress = address;
            if (address == null)
            {
                deliveryAddress = userAddress;
            }

            var orderedOn = DateTime.UtcNow.Date;
            var deliverDate = DateTime.UtcNow.AddDays(3);


            var order = new Order()
            {
                DeliveryAddress = deliveryAddress,
                EstimatedDeliveryDay = deliverDate,
                OrderedOn = orderedOn,
                ProductId = productId,
                Quantity = quantity,
                UserId = userId,
                Description = description
            };

            await this._ordersRepository.AddAsync(order);
            await this._ordersRepository.SaveChangesAsync();

            return true;
        }
    }
}
