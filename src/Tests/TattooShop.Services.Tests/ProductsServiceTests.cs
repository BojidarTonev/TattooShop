using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;
using Xunit;

namespace TattooShop.Services.Tests
{
    public class ProductsServiceTests
    {
        [Fact]
        public void AllShouldReturnTheCountOfAllProductsInTheDatabase()
        {
            var productsRepository = new Mock<IRepository<Product>>();
            productsRepository.Setup(r => r.All()).Returns(new List<Product>()
            {
                new Product(),
                new Product(),
                new Product()
            }.AsQueryable());

            var service = new ProductsService(productsRepository.Object, null);
            Assert.Equal(3, service.All().Count());
        }

        [Fact]
        public void GetAllCategoriesShouldActuallyReturnAllCategories()
        {
            var categoriesRepository = new Mock<IRepository<Category>>();
            categoriesRepository.Setup(r => r.All()).Returns(new List<Category>()
            {
                new Category()
            }.AsQueryable());

            var service = new ProductsService(null, categoriesRepository.Object);
            Assert.Single(service.GetAllCategories());
        }

        [Fact]
        public void GetAllProductsByCategoryShouldActuallyReturnAllProductsInTheCategory()
        {
            var categoriesRepository = new Mock<IRepository<Category>>();
            categoriesRepository.Setup(r => r.All()).Returns(new List<Category>()
            {
                new Category()
                {
                    Id = "1",
                    Name = ProductsCategories.TattooCare
                },
                new Category()
                {
                    Id = "2",
                    Name = ProductsCategories.Clothes
                }
            }.AsQueryable());

            var productsRepository = new Mock<IRepository<Product>>();
            productsRepository.Setup(r => r.All()).Returns(new List<Product>()
            {
                new Product()
                {
                    Category = categoriesRepository.Object.All().FirstOrDefault()
                },
                new Product()
                {
                    Category = categoriesRepository.Object.All().FirstOrDefault()
                },
                new Product()
                {
                    Category = categoriesRepository.Object.All().ElementAt(1)
                }
            }.AsQueryable());

            var service = new ProductsService(productsRepository.Object, categoriesRepository.Object);
            Assert.Equal(2, service.GetAllProductsByCategory(ProductsCategories.TattooCare.ToString()).Count());
        }

        [Fact]
        public void OtherSimilarShouldReturnOtherProductsWithTheSameCategory()
        {
            var categoriesRepository = new Mock<IRepository<Category>>();
            categoriesRepository.Setup(r => r.All()).Returns(new List<Category>()
            {
                new Category()
                {
                    Id = "1",
                    Name = ProductsCategories.TattooCare
                },
                new Category()
                {
                    Id = "2",
                    Name = ProductsCategories.Clothes
                }
            }.AsQueryable());

            var productsRepository = new Mock<IRepository<Product>>();
            productsRepository.Setup(r => r.All()).Returns(new List<Product>()
            {
                new Product()
                {
                    Category = categoriesRepository.Object.All().FirstOrDefault()
                },
                new Product()
                {
                    Category = categoriesRepository.Object.All().FirstOrDefault()
                },
                new Product()
                {
                    Category = categoriesRepository.Object.All().ElementAt(1)
                }
            }.AsQueryable());

            var service = new ProductsService(productsRepository.Object, categoriesRepository.Object);
            Assert.Equal(2, service.OtherSimilar(ProductsCategories.TattooCare.ToString()).Count());
        }
    }
}
