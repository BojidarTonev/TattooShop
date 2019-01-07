using AutoMapper;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Areas.Artists.Models
{
    public class BooksSuccesfullViewModel : IMapFrom<Artist>, IHaveCustomMappings
    {
        public string BookedFor { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Artist, BooksSuccesfullViewModel>()
                .ForMember(x => x.FirstName, m => m.MapFrom(p => p.FirstName));

            configuration.CreateMap<Artist, BooksSuccesfullViewModel>()
                .ForMember(x => x.LastName, m => m.MapFrom(p => p.LastName));
        }
    }
}
