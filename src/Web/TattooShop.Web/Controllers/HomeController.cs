using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TattooShop.Data;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;
using TattooShop.Web.Models;

namespace TattooShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            this._homeService = homeService;
        }

        public IActionResult Index()
        {
            var tattoos = this._homeService.RecentTattoos()
                .Select(t => new IndexTattooViewModel()
                {
                    Id = t.Id,
                    TattooUrl = t.TattooUrl,
                    TattooRelevantName = t.TattoRelevantName,
                    TattooStyle = t.TattooStyle.ToString()
                }).ToList();
            //var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //this._db.Orders.Add(new Order()
            //{
            //    UserId = userId,
            //    ProductId = "06a65418-83ae-4631-9305-ce4319927a37",
            //});
            //this._db.SaveChanges();
            return View(tattoos);
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        public IActionResult About()
        {
            var artists = this._homeService.AllArtists()
                .Select(a => new AboutArtistsDisplayViewModel()
                {
                    Autobiography = a.Autobiography,
                    FirstName = a.FirstName,
                    Id = a.Id,
                    LastName = a.LastName,
                    TattoosDone = a.TattoosDone.ToString(),
                    ImageUrl = a.ImageUrl
                }).ToList();

            return this.View(artists);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
