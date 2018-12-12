using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
            var tattoos = this._homeService.RecentTattoos();

            return View(tattoos);
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        public IActionResult About()
        {
            var artists = this._homeService.AllArtists();

            return this.View(artists);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
