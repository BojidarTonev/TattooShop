using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TattooShop.Web.Areas.Products.Models
{
    public class ProductDetailsViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<SimilarProductsDisplayModel> SimilarProducts { get; set; }
    }
}
