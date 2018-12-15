using System;
using System.Collections.Generic;
using System.Linq;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> _productsRepository;

        public ProductsService(IRepository<Product> productsRepository)
        {
            this._productsRepository = productsRepository;
        }

        public IEnumerable<Product> All() => this._productsRepository.All().ToList();

        public IEnumerable<Product> ProductsByCategory(string categoryName)
        {
            return null;
        }

        public Product ProductDetails(string productId)
        {
            var product = this._productsRepository.All().FirstOrDefault(p => p.Id == productId);

            return product;
        }

        public IEnumerable<Product> OtherSimilar(ProductsCategories category)
        {
            return this._productsRepository.All().Where(p => p.Category == category).Take(9);
        }

        public IEnumerable<ProductsCategories> GetAllCategories()
        {
            return new List<ProductsCategories>()
            {
                ProductsCategories.Clothes,
                ProductsCategories.Piercing,
                ProductsCategories.TattooCare
            };
        }
    }
}
