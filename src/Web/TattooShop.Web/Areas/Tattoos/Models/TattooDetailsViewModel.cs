using System.Collections.Generic;
using AutoMapper;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Areas.Tattoos.Models
{
    public class TattooDetailsViewModel : IMapFrom<Tattoo>, IHaveCustomMappings
    {
        public string ArtistId { get; set; }

        public string TattooUrl { get; set; }

        public string TattooRelevantName { get; set; }

        public string DoneOn { get; set; }

        public string Sessions { get; set; }

        public string TattooStyle { get; set; }

        public string ArtistFirstName { get; set; }

        public string ArtistLastName { get; set; }

        public ICollection<SimilarTattooViewModel> SimilarTattoos { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Tattoo, TattooDetailsViewModel>()
                .ForMember(x => x.ArtistId, m => m.MapFrom(t => t.ArtistId));

            configuration.CreateMap<Tattoo, TattooDetailsViewModel>()
                .ForMember(x => x.ArtistFirstName, m => m.MapFrom(t => t.Artist.FirstName));

            configuration.CreateMap<Tattoo, TattooDetailsViewModel>()
                .ForMember(x => x.ArtistLastName, m => m.MapFrom(t => t.Artist.LastName));

            configuration.CreateMap<Tattoo, TattooDetailsViewModel>()
                .ForMember(x => x.TattooStyle, m => m.MapFrom(s => s.TattooStyle.Name));

            //configuration.CreateMap<Tattoo, TattooDetailsViewModel>()
            //    .ForMember(x => x.DoneOn, m => m.MapFrom(x => x.DoneOn.ToString("d")));
        }
    }
}
