using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IArtistsService
    {
        IEnumerable<Artist> All();

        Artist Details(string id);

        Task<bool> AddBook(string bookedFor, string description, IFormFile file, string style, string userId, Artist artist);
    }
}
