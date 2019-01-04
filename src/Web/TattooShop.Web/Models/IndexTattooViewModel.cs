using AutoMapper;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Models
{
    public class IndexTattooViewModel : IMapFrom<Tattoo>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string TattooUrl { get; set; }

        public string TattooRelevantName { get; set; }

        public string TattooStyle { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Tattoo, IndexTattooViewModel>()
                .ForMember(x => x.TattooStyle, m => m.MapFrom(s => s.TattooStyle.Name));
        }
    }
}
