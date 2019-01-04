using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Category> _categoriesRepository;

        public ProductsService(IRepository<Product> productsRepository, IRepository<Category> categoriesRepository)
        {
            this._productsRepository = productsRepository;
            this._categoriesRepository = categoriesRepository;
        }

        public IQueryable<Product> All() => this._productsRepository.All().Include(p => p.Category);

        public Product ProductDetails(string productId)
        {
            var product = this._productsRepository.All().Include(p => p.Category).FirstOrDefault(p => p.Id == productId);

            return product;
        }

        public IQueryable<Product> OtherSimilar(ProductsCategories category)
        {
            return this._productsRepository.All().Include(p => p.Category).Where(p => p.Category.Name == category).Take(9);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return this._categoriesRepository.All().ToList();
        }

        public IQueryable<Product> GetAllProductsByCategory(string category)
        {
            return this._productsRepository.All().Where(p => p.Category.Name.ToString() == category);
        }
    }
}
