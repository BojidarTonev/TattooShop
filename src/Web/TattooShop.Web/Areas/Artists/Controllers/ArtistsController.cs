using Microsoft.AspNetCore.Mvc;
using TattooShop.Services.Contracts;

namespace TattooShop.Web.Areas.Artists.Controllers
{
    [Area("Artists")]
    public class ArtistsController : Controller
    {
        private readonly IArtistsService _artistsService;

        public ArtistsController(IArtistsService artistsService)
        {
            this._artistsService = artistsService;
        }

        public IActionResult All()
        {
            var artists = this._artistsService.All();

            return this.View(artists);
        }

        public IActionResult Details(string id)
        {
            var artist = this._artistsService.Details(id);

            return View(artist);
        }

        public IActionResult BookTattoo(string id)
        {
            var artist = this._artistsService.Details(id);
            return this.View(artist);
        }
    }
}