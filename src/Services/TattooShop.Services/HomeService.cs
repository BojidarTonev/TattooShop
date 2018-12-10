using System;
using System.Collections.Generic;
using System.Linq;
using TattooShop.Data;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class HomeService : IHomeService
    {
        private readonly TattooShopContext _context;
        private const int TattoosToTake = 12;

        public HomeService(TattooShopContext context)
        {
            this._context = context;
        }

        public IEnumerable<Tattoo> RecentTattoos()
        {
            return this._context.Tattoos.OrderBy(x => x.DoneOn).Take(TattoosToTake).ToList();
        }
    }
}
