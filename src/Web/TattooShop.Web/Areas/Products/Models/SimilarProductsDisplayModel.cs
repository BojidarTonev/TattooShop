using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Areas.Products.Models
{
    public class SimilarProductsDisplayModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, SimilarProductsDisplayModel>()
                .ForMember(x => x.Category, m => m.MapFrom(c => c.Category.Name));
        }
    }
}
