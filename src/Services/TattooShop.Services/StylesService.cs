using System.Linq;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class StylesService : IStylesService
    {
        private readonly IRepository<Style> _stylesRepository;

        public StylesService(IRepository<Style> stylesRepository)
        {
            this._stylesRepository = stylesRepository;
        }

        public Style GetStyle(string id)
        {
            var style = this._stylesRepository.All().FirstOrDefault(s => s.Id == id);

            return style;
        }
    }
}
