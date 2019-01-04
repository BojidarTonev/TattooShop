using AutoMapper;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Areas.Artists.Models
{
    public class DisplayAllArtistsViewModel : IMapFrom<Artist>, IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BestAt { get; set; }

        public string Autobiography { get; set; }

        public string ArtistId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Artist, DisplayAllArtistsViewModel>()
                .ForMember(x => x.ArtistId, m => m.MapFrom(a => a.Id));
        }
    }
}
