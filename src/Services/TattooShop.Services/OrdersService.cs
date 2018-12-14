using System;
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

        public async Task<bool> AddOrder(string address, string description, int quantity, Product product, TattooShopUser user)
        {
            if (description == null ||
                product == null ||
                user == null)
            {
                return true;
            }

            string deliveryAddress = address;
            if (address == null)
            {
                deliveryAddress = user.Address;
            }

            var orderedOn = DateTime.UtcNow;
            var deliverDate = DateTime.UtcNow.AddDays(3);

            var order = new Order()
            {
                DeliveryAddress = address,
                EstimatedDeliveryDay = deliverDate,
                OrderedOn = orderedOn,
                Product = product,
                Quantity = quantity,
                User = user,
                Description = description
            };

            await this._ordersRepository.AddAsync(order);
            await this._ordersRepository.SaveChangesAsync();

            return true;
        }
    }
}
