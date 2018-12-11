using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
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
    }
}
