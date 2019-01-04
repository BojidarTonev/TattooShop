using System.Collections.Generic;
using System.Linq;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IProductsService
    {
        IQueryable<Product> All();

        TViewModel ProductDetails<TViewModel>(string productId);

        IQueryable<Product> OtherSimilar(string category);

        IEnumerable<Category> GetAllCategories();

        IQueryable<Product> GetAllProductsByCategory(string category);
    }
}
