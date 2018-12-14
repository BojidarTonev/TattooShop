using System;
using System.Collections.Generic;
using System.Text;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IUsersService
    {
        IEnumerable<Book> GetUserBooks(string userId);

        IEnumerable<Order> GetUserOrders(string userId);
    }
}
