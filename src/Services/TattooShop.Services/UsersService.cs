using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<TattooShopUser> _usersRepository;

        public UsersService(IRepository<Book> booksRepository, IRepository<Order> ordersRepository, IRepository<TattooShopUser> usersRepository)
        {
            this._booksRepository = booksRepository;
            this._ordersRepository = ordersRepository;
            this._usersRepository = usersRepository;
        }

        public IEnumerable<Book> GetUserBooks(string userId)
        {
            var userBooks = this._booksRepository.All().Where(b => b.UserId == userId).Include(x => x.Artist);
            return userBooks;
        }

        public IEnumerable<Order> GetUserOrders(string userId)
        {
            var userOrders = this._ordersRepository.All().Where(o => o.UserId == userId).Include(x => x.Product);
            return userOrders;
        }

        public string GetUserAddress(string id)
        {
            var user = this._usersRepository.All().Single(u => u.Id == id);

            return user.Address;
        }
    }
}
