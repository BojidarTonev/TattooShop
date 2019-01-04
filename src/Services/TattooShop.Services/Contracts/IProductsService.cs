using System.Collections.Generic;
using System.Linq;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;

namespace TattooShop.Services.Contracts
{
    public interface IProductsService
    {
        IQueryable<Product> All();

        Product ProductDetails(string productId);

        IQueryable<Product> OtherSimilar(ProductsCategories category);

        IEnumerable<Category> GetAllCategories();

        IQueryable<Product> GetAllProductsByCategory(string category);
    }
}
