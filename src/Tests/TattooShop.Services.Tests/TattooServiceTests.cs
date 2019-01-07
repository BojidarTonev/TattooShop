using System.Collections.Generic;
using System.Linq;
using Moq;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;
using Xunit;

namespace TattooShop.Services.Tests
{
    public class TattooServiceTests
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
            
            var tattoosRepository = new Mock<IRepository<Tattoo>>();
            tattoosRepository.Setup(r => r.All()).Returns(new List<Tattoo>()
            {
                new Tattoo(),
                new Tattoo(),
                new Tattoo()
            }.AsQueryable());

            var service = new ArtistsService(artistsRepository.Object, null, null, tattoosRepository.Object);
            Assert.Equal(3, service.All().Count());
        }

        [Fact]
        public void OtherSimilarShouldActuallyReturnOtherSimilarTattoos()
        {
            var stylesRepository = new Mock<IRepository<Style>>();
            stylesRepository.Setup(r => r.All()).Returns(new List<Style>()
            {
                new Style()
                {
                    Id = "1",
                    Name = TattooStyles.Geometric
                },
                new Style()
                {
                    Id = "2",
                    Name = TattooStyles.Realistic
                }
            }.AsQueryable());

            var tattoosRepository = new Mock<IRepository<Tattoo>>();
            tattoosRepository.Setup(r => r.All()).Returns(new List<Tattoo>()
            {
                new Tattoo()
                {
                    TattooStyle = stylesRepository.Object.All().FirstOrDefault()
                },
                new Tattoo()
                {
                    TattooStyle = stylesRepository.Object.All().FirstOrDefault()
                },
                new Tattoo()
                {
                    TattooStyle = stylesRepository.Object.All().ElementAt(1)
                }
            }.AsQueryable());

            var service = new TattoosService(tattoosRepository.Object, stylesRepository.Object);
            Assert.Equal(2, service.OtherSimilar(TattooStyles.Geometric.ToString()).Count());

        }

        [Fact]
        public void GetAllStylesShouldActuallyReturnAllStyles()
        {
            var stylesRepository = new Mock<IRepository<Style>>();
            stylesRepository.Setup(r => r.All()).Returns(new List<Style>()
            {
                new Style()
            }.AsQueryable());

            var service = new TattoosService(null, stylesRepository.Object);
            Assert.Single(service.GetAllStyles());
        }

        [Fact]
        public void GetAllTattoosFromStyleShouldActuallyReturnTattoosFromTheSameStyle()
        {
            var stylesRepository = new Mock<IRepository<Style>>();
            stylesRepository.Setup(r => r.All()).Returns(new List<Style>()
            {
                new Style()
                {
                    Id = "1",
                    Name = TattooStyles.Realistic
                },
                new Style()
                {
                    Id = "2",
                    Name = TattooStyles.Geometric
                }
            }.AsQueryable());

            var tattoosRepository = new Mock<IRepository<Tattoo>>();
            tattoosRepository.Setup(r => r.All()).Returns(new List<Tattoo>()
            {
                new Tattoo()
                {
                    TattooStyle = stylesRepository.Object.All().FirstOrDefault()
                },
                new Tattoo()
                {
                    TattooStyle = stylesRepository.Object.All().FirstOrDefault()
                },
                new Tattoo()
                {
                    TattooStyle = stylesRepository.Object.All().ElementAt(1)
                }
            }.AsQueryable());

            var service = new TattoosService(tattoosRepository.Object, stylesRepository.Object);
            Assert.Equal(2, service.GetAllTattoosFromStyle(TattooStyles.Realistic.ToString()).Count());
        }
    }
}
