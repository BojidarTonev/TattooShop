using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class HomeService : IHomeService
    {
        private readonly IRepository<Tattoo> _tattoosRepository;
        private readonly IRepository<Artist> _artistsRepository;
        private const int TattoosToTake = 12;

        public HomeService(IRepository<Tattoo> tattoosRepository, IRepository<Artist> artistsRepository)
        {
            this._tattoosRepository = tattoosRepository;
            this._artistsRepository = artistsRepository;
        }

        public IEnumerable<Tattoo> RecentTattoos()
        {
            return this._tattoosRepository.All().Include(t => t.TattooStyle).Take(TattoosToTake).OrderBy(x => x.DoneOn).ToList();
        }

        public IEnumerable<Artist> AllArtists()
        {
            return this._artistsRepository.All().Include(a => a.TattooCollection).ToList();
        }
    }
}
