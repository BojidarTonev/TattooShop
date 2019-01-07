using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Areas.Artists.Models
{
    public class BookTattooInputViewModel : IMapFrom<Artist>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Please select date in the near future, we are not wizard, we cant go back in time, neither predict your wishes.")]
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
