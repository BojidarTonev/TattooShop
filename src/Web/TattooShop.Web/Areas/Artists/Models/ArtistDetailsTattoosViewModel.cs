using AutoMapper;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Areas.Artists.Models
{
    public class ArtistDetailsTattoosViewModel : IMapFrom<Tattoo>, IHaveCustomMappings
    {
        public string TattooId { get; set; }

        public string TattooUrl { get; set; }

        public string TattooRelevantName { get; set; }

        public string TattooStyle { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Tattoo, ArtistDetailsTattoosViewModel>()
                .ForMember(x => x.TattooStyle, m => m.MapFrom(x => x.TattooStyle.Name));

            configuration.CreateMap<Tattoo, ArtistDetailsTattoosViewModel>()
                .ForMember(x => x.TattooId, m => m.MapFrom(a => a.Id));
        }
    }
}
