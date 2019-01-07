using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data;
using TattooShop.Data.Models;
using Xunit;

namespace TattooShop.Services.Tests
{
    public class StylesServiceTests
    {
        [Fact]
        public void GetStyleShouldReturnTheRightCategory()
        {
            var options = new DbContextOptionsBuilder<TattooShopContext>()
                .UseInMemoryDatabase(databaseName: "Unique_Db_Name_5785219")
                .Options;
            var dbContext = new TattooShopContext(options);
            dbContext.Styles.Add(new Style()
            {
                Id = "1"
            });
            dbContext.SaveChanges();

            var stylesRepository = new DbRepository<Style>(dbContext);
            var stylesService = new StylesService(stylesRepository);
            var style = stylesService.GetStyle("1");

            Assert.Equal("1", style.Id);
        }
    }
}
