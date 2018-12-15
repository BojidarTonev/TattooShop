using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TattooShop.Web.Areas.Products.Models
{
    public class SimilarProductsDisplayModel
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }
    }
}
