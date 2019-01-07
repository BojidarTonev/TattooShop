using System.Collections.Generic;
using System.Linq;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface ITattoosService
    {
        IQueryable<Tattoo> All();

        TViewModel Details<TViewModel>(string id);

        IQueryable<Tattoo> OtherSimilar(string tattooStyle);

        IEnumerable<Style> GetAllStyles();

        IQueryable<Tattoo> GetAllTattoosFromStyle(string style);
    }
}
