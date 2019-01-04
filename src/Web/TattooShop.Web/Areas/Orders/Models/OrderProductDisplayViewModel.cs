using AutoMapper;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Areas.Orders.Models
{
    public class OrderProductDisplayViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Price { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, OrderProductDisplayViewModel>()
                .ForMember(x => x.Category, m => m.MapFrom(p => p.Category.Name));
        }
    }
}
