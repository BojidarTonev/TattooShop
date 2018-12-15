using System;
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

        public TattoosService(IRepository<Tattoo> tattoosRepository)
        {
            this._tattoosRepository = tattoosRepository;
        }

        public IEnumerable<Tattoo> All() => this._tattoosRepository.All().OrderBy(t => t.DoneOn);

        public Tattoo Details(string id)
        {
            var tattoo = this._tattoosRepository.All().Include(t => t.Artist).First(t => t.Id == id);

            return tattoo;
        }

        public IEnumerable<Tattoo> OtherSimilar(TattooStyles tattooStyle)
        {
            return this._tattoosRepository.All().Where(t => t.TattooStyle == tattooStyle).OrderBy(t => t.DoneOn).Take(9)
                .ToList();
        }

        public IEnumerable<TattooStyles> GetAllStyles()
        {
            return new List<TattooStyles>()
            {
                TattooStyles.AmericanTraditional,
                TattooStyles.Biomechanical,
                TattooStyles.Geometric,
                TattooStyles.Polynesian,
                TattooStyles.Realistic,
                TattooStyles.TraditionalJapanese,
                TattooStyles.Watercolor
            };
        }
    }
}
