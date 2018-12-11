using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class ArtistsService : IArtistsService
    {
        private readonly IRepository<Artist> _artistsRepository;

        public ArtistsService(IRepository<Artist> artistsRepository)
        {
            this._artistsRepository = artistsRepository;
        }

        public IEnumerable<Artist> All() => this._artistsRepository.All().ToList();

        public Artist Details(string id)
        {
            var artist = this._artistsRepository.All()
                .Include(t => t.TattooCollection)
                .FirstOrDefault(a => a.Id == id);

            return artist;
        }

        public async Task<bool> AddBook(Artist artist, string bookedFor, string description, IFormFile file, ClaimsPrincipal user)
        {
            return true;
        }
    }
}
