﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class ArtistsService : IArtistsService
    {
        private readonly IRepository<Artist> _artistsRepository;
        private readonly IRepository<Book> _booksRepository;
        private readonly IImageService _imageService;

        public ArtistsService(IRepository<Artist> artistsRepository, IImageService imageService, IRepository<Book> booksRepository)
        {
            this._artistsRepository = artistsRepository;
            this._imageService = imageService;
            this._booksRepository = booksRepository;
        }

        public IQueryable<Artist> All() => this._artistsRepository.All();

        public Artist Details(string id)
        {
            var artist = this._artistsRepository.All()
                .Include(t => t.TattooCollection)
                .ThenInclude(t => t.TattooStyle)
                .FirstOrDefault(a => a.Id == id);

            return artist;
        }

        public async Task<bool> AddBook(string bookedFor, string description, IFormFile file, string style, string userId, Artist artist)
        {
            if (bookedFor == null ||
                description == null ||
                file == null ||
                style == null ||
                userId == null ||
                artist == null)
            {
                return false;
            }

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
    }
}
