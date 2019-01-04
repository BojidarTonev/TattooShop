using System.Collections.Generic;
using System.Linq;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;

namespace TattooShop.Services.Contracts
{
    public interface ITattoosService
    {
        IQueryable<Tattoo> All();

        Tattoo Details(string id);

        IEnumerable<Tattoo> OtherSimilar(TattooStyles tattooStyle);

        IEnumerable<Style> GetAllStyles();

        IQueryable<Tattoo> GetAllTattoosFromStyle(string style);
    }
}
