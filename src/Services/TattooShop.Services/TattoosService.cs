using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;
using TattooShop.Services.Automapper;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class TattoosService : ITattoosService
    {
        private readonly IRepository<Tattoo> _tattoosRepository;
        private readonly IRepository<Style> _stylesRepository;
        private const int TattoosToTake = 9;

        public TattoosService(IRepository<Tattoo> tattoosRepository, IRepository<Style> stylesRepository)
        {
            this._tattoosRepository = tattoosRepository;
            this._stylesRepository = stylesRepository;
        }

        public IQueryable<Tattoo> All() => this._tattoosRepository.All().Include(t => t.TattooStyle).OrderBy(t => t.DoneOn);

        public TViewModel Details<TViewModel>(string id)
        {
            var tattoo = this._tattoosRepository.All().Include(t => t.TattooStyle).ThenInclude(t => t.Name).Include(t => t.Artist).Where(t => t.Id == id)
                .To<TViewModel>()
                .FirstOrDefault();

            return tattoo;
        }

        public IQueryable<Tattoo> OtherSimilar(string tattooStyle)
        {
            var style = Enum.Parse<TattooStyles>(tattooStyle);
            return this._tattoosRepository.All().Include(t => t.TattooStyle)
                .Where(t => t.TattooStyle.Name == style).OrderBy(t => t.DoneOn).Take(TattoosToTake);
        }

        public IEnumerable<Style> GetAllStyles()
        {
            return this._stylesRepository.All().ToList();
        }

        public IQueryable<Tattoo> GetAllTattoosFromStyle(string style)
        {
            return this._tattoosRepository.All().Where(t => t.TattooStyle.Name.ToString() == style);
        }
    }
}
