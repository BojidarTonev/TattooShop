using System.Collections.Generic;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;

namespace TattooShop.Services.Contracts
{
    public interface ITattoosService
    {
        IEnumerable<Tattoo> All();

        Tattoo Details(string id);

        IEnumerable<Tattoo> OtherSimilar(string tattooStyle);

        IEnumerable<TattooStyles> GetAllStyles();
    }
}
