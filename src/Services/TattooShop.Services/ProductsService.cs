using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;
using TattooShop.Services.Automapper;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private const int OrdersToTake = 9;

        public ProductsService(IRepository<Product> productsRepository, IRepository<Category> categoriesRepository)
        {
            this._productsRepository = productsRepository;
            this._categoriesRepository = categoriesRepository;
        }

        public IQueryable<Product> All() => this._productsRepository.All().Include(p => p.Category);

        public TViewModel ProductDetails<TViewModel>(string productId)
        {
            var product = this._productsRepository.All().Include(p => p.Category)
                .Where(p => p.Id == productId)
                .To<TViewModel>()
                .FirstOrDefault();

            return product;
        }

        public IQueryable<Product> OtherSimilar(string category)
        {
            var productCategory = Enum.Parse<ProductsCategories>(category);

            return this._productsRepository.All().Include(p => p.Category).Where(p => p.Category.Name == productCategory).Take(OrdersToTake);
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
