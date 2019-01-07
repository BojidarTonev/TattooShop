using Microsoft.EntityFrameworkCore;
using TattooShop.Data;
using TattooShop.Data.Models;
using Xunit;

namespace TattooShop.Services.Tests
{
    public class CategoriesServiceTests
    {
        [Fact]
        public void GetCategoryShouldReturnTheRightCategory()
        {
            var options = new DbContextOptionsBuilder<TattooShopContext>()
                .UseInMemoryDatabase(databaseName: "Unique_Db_Name_5785219")
                .Options;
            var dbContext = new TattooShopContext(options);
            dbContext.Categories.Add(new Category()
            {
                Id = "1"
            });
            dbContext.SaveChanges();

            var categoriesRepository = new DbRepository<Category>(dbContext);
            var categoriesService = new CategoriesService(categoriesRepository);
            var category = categoriesService.GetCategory("1");

            Assert.Equal("1", category.Id);
        }
    }
}
