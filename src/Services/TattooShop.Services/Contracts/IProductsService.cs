using System.Collections.Generic;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IProductsService
    {
        IEnumerable<Product> All();

        IEnumerable<Product> ProductsByCategory(string categoryName);

        Product ProductDetails(string productId);
    }
}
