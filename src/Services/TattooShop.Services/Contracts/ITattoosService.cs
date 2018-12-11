using System.Collections.Generic;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface ITattoosService
    {
        IEnumerable<Tattoo> All();

        Tattoo Details(string id);
    }
}
