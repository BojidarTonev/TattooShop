using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using TattooShop.Data;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using Xunit;

namespace TattooShop.Services.Tests
{
    public class HomeServiceTests
    {
        [Fact]
        public void RecentTattoosShouldReturnTwelveTattoosMaximum()
        {
            var tattoosService = new Mock<IRepository<Tattoo>>();
            tattoosService.Setup(r => r.All()).Returns(new List<Tattoo>()
            {
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo(),
                new Tattoo()
            }.AsQueryable());

            var service = new HomeService(tattoosService.Object, null, null);
            Assert.Equal(12, service.RecentTattoos().Count());
        }

        [Fact]
        public void AllArtistsShouldActuallyReturnAllArtists()
        {
            var artistsRepository = new Mock<IRepository<Artist>>();
            artistsRepository.Setup(r => r.All()).Returns(new List<Artist>()
            {
                new Artist(),
                new Artist(),
                new Artist()
            }.AsQueryable());

            var service = new HomeService(null, artistsRepository.Object, null);
            Assert.Equal(3, service.AllArtists().Count());
        }

        [Fact]
        public void RegisterFeedBackShouldActuallyRegistersFeedback()
        {
            var options = new DbContextOptionsBuilder<TattooShopContext>()
                .UseInMemoryDatabase(databaseName: "Unique_Db_Name_57852181")
                .Options;
            var dbContext = new TattooShopContext(options);

            var feedbackRepository = new DbRepository<ContactInfo>(dbContext);
            var homeService = new HomeService(null, null, feedbackRepository);

            homeService.RegisterFeedBack("Kaloqn", "Kaloqnov", "mn qk sait", "Kaloqn@kaloqn.kaloqn", "975649867");
            Assert.Equal(1, feedbackRepository.All().Count());
        }

        [Fact]
        public void AllFeedbackActuallyReturnsAllOfTheFeedback()
        {
            var feedbackRepository = new Mock<IRepository<ContactInfo>>();
            feedbackRepository.Setup(r => r.All()).Returns(new List<ContactInfo>()
            {
                new ContactInfo(),
                new ContactInfo(),
                new ContactInfo()
            }.AsQueryable());

            var service = new HomeService(null, null, feedbackRepository.Object);
            Assert.Equal(3, service.AllFeedback().Count());
        }
    }
}
