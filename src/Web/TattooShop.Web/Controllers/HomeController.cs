using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TattooShop.Services.Automapper;
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
                .To<IndexTattooViewModel>()
                .ToList();

            return View(tattoos);
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (!TryValidateModel(model))
            {
                return this.View(model);
            }

            this._homeService.RegisterFeedBack(model.FirstName, model.LastName, model.Message, model.SenderEmail, model.SenderPhoneNumber);

            return this.View();
        }

        public IActionResult About()
        {
            var artists = this._homeService.AllArtists()
                .To<AboutArtistsDisplayViewModel>()
                .ToList();

            return this.View(artists);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
