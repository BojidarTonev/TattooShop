using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;
using TattooShop.Services.Automapper;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class ArtistsService : IArtistsService
    {
        private readonly IRepository<Artist> _artistsRepository;
        private readonly IRepository<Book> _booksRepository;
        private readonly IRepository<Tattoo> _tattoosRepository;
        private readonly IImageService _imageService;

        public ArtistsService(IRepository<Artist> artistsRepository, IImageService imageService, IRepository<Book> booksRepository, IRepository<Tattoo> tattoosRepository)
        {
            this._artistsRepository = artistsRepository;
            this._imageService = imageService;
            this._booksRepository = booksRepository;
            this._tattoosRepository = tattoosRepository;
        }

        public IQueryable<Artist> All() => this._artistsRepository.All();

        public TViewModel Details<TViewModel>(string id)
        {
            var artist = this._artistsRepository.All()
                .Include(t => t.TattooCollection)
                .Where(t => t.Id == id)
                .To<TViewModel>()
                .FirstOrDefault();

            return artist;
        }

        public async Task<bool> AddBook(string bookedFor, string description, IFormFile file, string style, string userId, string artistId)
        {
            if (bookedFor == null ||
                description == null ||
                file == null ||
                style == null ||
                userId == null ||
                artistId == null)
            {
                return false;
            }

            var artist = this._artistsRepository.All().FirstOrDefault(a => a.Id == artistId);

            var Style = Enum.Parse<TattooStyles>(style);
            var imageUrl = this._imageService.AddToCloudinaryAndReturnImageUrl(file);

            var BookedFor = DateTime.Parse(bookedFor);
            if (DateTime.UtcNow > BookedFor)
            {
                return false;
            }

            var book = new Book()
            {
                UserId = userId,
                Artist = artist,
                BookedFor = BookedFor,
                BookedOn = DateTime.UtcNow,
                Description = description,
                ImageUrl = imageUrl,
                TattooStyle = Style
            };

            await this._booksRepository.AddAsync(book);
            await this._booksRepository.SaveChangesAsync();

            return true;
        }

        public IEnumerable<TViewModel> GetArtistsArtwork<TViewModel>(string id)
        {
            var artistTattoos = this._tattoosRepository.All()
                .Where(t => t.ArtistId == id)
                .OrderByDescending(x => x.DoneOn)
                .Include(t => t.TattooStyle)
                .ThenInclude(t => t.Name)
                .To<TViewModel>()
                .ToList();

            return artistTattoos;
        }
    }
}
