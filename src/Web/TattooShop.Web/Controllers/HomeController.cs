using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
        private readonly TattooShopContext _db;
        private readonly UserManager<TattooShopUser> _userManager;

        public HomeController(IHomeService homeService, TattooShopContext db, UserManager<TattooShopUser> userManager)
        {
            this._homeService = homeService;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var tattoos = this._homeService.RecentTattoos();
            //var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //this._db.Orders.Add(new Order()
            //{
            //    UserId = userId,
            //    ProductId = "06a65418-83ae-4631-9305-ce4319927a37",
            //});
            this._db.SaveChanges();
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
