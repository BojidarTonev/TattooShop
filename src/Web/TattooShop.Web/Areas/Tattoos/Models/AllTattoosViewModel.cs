using AutoMapper;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Areas.Tattoos.Models
{
    public class AllTattoosViewModel : IMapFrom<Tattoo>, IHaveCustomMappings

    {
        public string Id { get; set; }

        public string TattooUrl { get; set; }

        public string TattooRelevantName { get; set; }

        public string TattooStyle { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Tattoo, AllTattoosViewModel>()
                .ForMember(x => x.TattooStyle, m => m.MapFrom(s => s.TattooStyle.Name));
        }
    }
}
