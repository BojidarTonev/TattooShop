using System.Collections.Generic;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IHomeService
    {
        IEnumerable<Tattoo> RecentTattoos();
    }
}
