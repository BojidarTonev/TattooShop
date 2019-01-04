using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Models
{
    public class AboutArtistsDisplayViewModel : IMapFrom<Artist>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string TattoosDone { get; set; }

        public string Autobiography { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Artist, AboutArtistsDisplayViewModel>()
                .ForMember(x => x.TattoosDone, m => m.MapFrom(c => c.TattooCollection.Count));
        }
    }
}
