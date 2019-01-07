using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data;
using TattooShop.Data.Models;
using Xunit;

namespace TattooShop.Services.Tests
{
    public class OrdersServiceTests
    {
        [Fact]
        public async Task AddOrderShouldActuallyAddOrder()
        {
            var options = new DbContextOptionsBuilder<TattooShopContext>()
                .UseInMemoryDatabase(databaseName: "Unique_Db_Name_5785219")
                .Options;
            var dbContext = new TattooShopContext(options);
            dbContext.Products.Add(new Product()
            {
                Id = "1"
            });
            dbContext.Users.Add(new TattooShopUser()
            {
                Id = "2"
            });
            dbContext.SaveChanges();

            var ordersRepository = new DbRepository<Order>(dbContext);
            var ordersService = new OrdersService(ordersRepository);

            await ordersService.AddOrder("sample address", "sample description", 3, "1", "2");
            Assert.Equal(1, dbContext.Orders.Count());
        }
    }
}
