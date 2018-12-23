using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class TattoosService : ITattoosService
    {
        private readonly IRepository<Tattoo> _tattoosRepository;
        private readonly IRepository<Style> _stylesRepository;

        public TattoosService(IRepository<Tattoo> tattoosRepository, IRepository<Style> stylesRepository)
        {
            this._tattoosRepository = tattoosRepository;
            this._stylesRepository = stylesRepository;
        }

        public IEnumerable<Tattoo> All() => this._tattoosRepository.All().Include(t => t.TattooStyle).OrderBy(t => t.DoneOn);

        public Tattoo Details(string id)
        {
            var tattoo = this._tattoosRepository.All().Include(t => t.TattooStyle).Include(t => t.Artist).First(t => t.Id == id);

            return tattoo;
        }

        public IEnumerable<Tattoo> OtherSimilar(TattooStyles tattooStyle)
        {
            return this._tattoosRepository.All().Include(t => t.TattooStyle).Where(t => t.TattooStyle.Name == tattooStyle).OrderBy(t => t.DoneOn).Take(9)
                .ToList();
        }

        public IEnumerable<Style> GetAllStyles()
        {
            return this._stylesRepository.All().ToList();
        }

        public IEnumerable<Tattoo> GetAllTattoosFromStyle(string style)
        {
            return this._tattoosRepository.All().Where(t => t.TattooStyle.Name.ToString() == style).ToList();
        }
    }
}
