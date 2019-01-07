using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using TattooShop.Data;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using Xunit;

namespace TattooShop.Services.Tests
{
    public class ArtistsServiceTests
    {
        [Fact]
        public void AllShouldReturnCorrectNumber()
        {
            var artistsRepository = new Mock<IRepository<Artist>>();
            artistsRepository.Setup(r => r.All()).Returns(new List<Artist>()
            {
                new Artist(),
                new Artist(),
                new Artist()
            }.AsQueryable());

            var service = new ArtistsService(artistsRepository.Object, null, null, null);
            Assert.Equal(3, service.All().Count());
        }

        [Fact]
        public async Task AddBookShouldActuallyAddBookToDatabase()
        {
            var options = new DbContextOptionsBuilder<TattooShopContext>()
                .UseInMemoryDatabase(databaseName: "Unique_Db_Name_5785219")
                .Options;
            var dbContext = new TattooShopContext(options);
            dbContext.Artists.Add(new Artist()
            {
                Id = "1"
            });
            dbContext.Users.Add(new TattooShopUser()
            {
                Id = "2"
            });
            dbContext.SaveChanges();

            var artistsRepository = new DbRepository<Artist>(dbContext);
            var booksRepository = new DbRepository<Book>(dbContext);
            var imageService = new ImageService(dbContext);
            var tattoosRepository = new DbRepository<Tattoo>(dbContext);

            var artistsService = new ArtistsService(artistsRepository, imageService, booksRepository, tattoosRepository);
            var fileMock = new Mock<IFormFile>();
            var content = "Hallo world from a fake file!";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            fileMock.Setup(x => x.FileName).Returns(fileName);
            fileMock.Setup(x => x.Length).Returns(ms.Length);

            await artistsService.AddBook(DateTime.UtcNow.AddDays(3).ToString(), "short description", fileMock.Object, "Geometric",
                "2", "1");
            
            Assert.Equal(1, booksRepository.All().Count());
        }

    }
}
