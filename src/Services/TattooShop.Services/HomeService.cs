using System.Collections.Generic;
using System.Linq;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class HomeService : IHomeService
    {
        private readonly IRepository<Tattoo> _tattoosRepository;
        private const int TattoosToTake = 12;

        public HomeService(IRepository<Tattoo> tattoosRepository)
        {
            this._tattoosRepository = tattoosRepository;
        }

        public IEnumerable<Tattoo> RecentTattoos()
        {
            return this._tattoosRepository.All().Take(TattoosToTake).OrderBy(x => x.DoneOn).ToList();
        }
    }
}
