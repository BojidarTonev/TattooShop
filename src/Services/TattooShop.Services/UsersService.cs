using System.Collections.Generic;
using System.Linq;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly IRepository<Order> _ordersRepository;

        public UsersService(IRepository<Book> booksRepository, IRepository<Order> ordersRepository)
        {
            this._booksRepository = booksRepository;
            this._ordersRepository = ordersRepository;
        }

        public IEnumerable<Book> GetUserBooks(string userId)
        {
            var userBooks = this._booksRepository.All().Where(b => b.UserId == userId);
            return userBooks;
        }

        public IEnumerable<Order> GetUserOrders(string userId)
        {
            var userOrders = this._ordersRepository.All().Where(o => o.UserId == userId);
            return userOrders;
        }
    }
}
