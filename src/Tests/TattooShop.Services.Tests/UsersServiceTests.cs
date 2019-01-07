using System.Collections.Generic;
using System.Linq;
using Moq;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using Xunit;

namespace TattooShop.Services.Tests
{
    public class UsersServiceTests
    {
        [Fact]
        public void GetUserBooksShouldReturnAllUserBooks()
        {
            var usersRepository = new Mock<IRepository<TattooShopUser>>();
            usersRepository.Setup(r => r.All()).Returns(new List<TattooShopUser>()
            {
                new TattooShopUser()
                {
                    Id = "1"
                }
            }.AsQueryable());

            var booksRepository = new Mock<IRepository<Book>>();
            booksRepository.Setup(r => r.All()).Returns(new List<Book>()
            {
                new Book()
                {
                    Id = "2",
                    UserId = "1"
                },
                new Book()
                {
                    Id = "3",
                    UserId = "1"
                }
            }.AsQueryable());

            var service = new UsersService(booksRepository.Object, null, usersRepository.Object);
            Assert.Equal(2, service.GetUserBooks("1").Count());
        }

        [Fact]
        public void GetUserOrdersShouldReturnAllUserOrders()
        {
            var usersRepository = new Mock<IRepository<TattooShopUser>>();
            usersRepository.Setup(r => r.All()).Returns(new List<TattooShopUser>()
            {
                new TattooShopUser()
                {
                    Id = "1"
                }
            }.AsQueryable());

            var ordersRepository = new Mock<IRepository<Order>>();
            ordersRepository.Setup(r => r.All()).Returns(new List<Order>()
            {
                new Order()
                {
                    Id = "2",
                    UserId = "1"
                },
                new Order()
                {
                    Id = "3",
                    UserId = "1"
                }
            }.AsQueryable());

            var service = new UsersService(null, ordersRepository.Object, usersRepository.Object);
            Assert.Equal(2, service.GetUserOrders("1").Count());
        }

        [Fact]
        public void GetUserAddressShouldReturnUsersValidAddress()
        {
            var usersRepository = new Mock<IRepository<TattooShopUser>>();
            usersRepository.Setup(r => r.All()).Returns(new List<TattooShopUser>()
            {
                new TattooShopUser()
                {
                    Id = "1",
                    Address = "Sample address"
                }
            }.AsQueryable());

            var service = new UsersService(null, null, usersRepository.Object);
            Assert.Same("Sample address", service.GetUserAddress("1"));
        }

        [Fact]
        public void GetUserEmailShouldReturnUserValidEmail()
        {
            var usersRepository = new Mock<IRepository<TattooShopUser>>();
            usersRepository.Setup(r => r.All()).Returns(new List<TattooShopUser>()
            {
                new TattooShopUser()
                {
                    Id = "1",
                    Email = "Sample address"
                }
            }.AsQueryable());

            var service = new UsersService(null, null, usersRepository.Object);
            Assert.Same("Sample address", service.GetUserEmail("1"));
        }
    }
}
