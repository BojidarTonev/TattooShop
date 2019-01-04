using AutoMapper;
using Microsoft.AspNetCore.Http;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Areas.Artists.Models
{
    public class BookTattooInputViewModel : IMapFrom<Artist>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public string BookedFor { get; set; }

        public string Style { get; set; }

        public string ArtistImageUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Artist, BookTattooInputViewModel>()
                .ForMember(x => x.ArtistImageUrl, m => m.MapFrom(t => t.ImageUrl));
        }
    }
}
