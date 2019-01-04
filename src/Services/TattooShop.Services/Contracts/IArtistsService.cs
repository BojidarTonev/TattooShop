using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IArtistsService
    {
        IQueryable<Artist> All();

        TViewModel Details<TViewModel>(string id);

        Task<bool> AddBook(string bookedFor, string description, IFormFile file, string style, string userId, string artistId);

        IEnumerable<TViewModel> GetArtistsArtwork<TViewModel>(string id);
    }
}
