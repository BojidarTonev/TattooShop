using System.Collections.Generic;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;

namespace TattooShop.Services.Contracts
{
    public interface IProductsService
    {
        IEnumerable<Product> All();

        IEnumerable<Product> ProductsByCategory(string categoryName);

        Product ProductDetails(string productId);

        IEnumerable<Product> OtherSimilar(ProductsCategories category);

        IEnumerable<ProductsCategories> GetAllCategories();
    }
}
