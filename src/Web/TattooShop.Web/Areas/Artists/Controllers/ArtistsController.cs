using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TattooShop.Services.Contracts;

namespace TattooShop.Web.Areas.Artists.Controllers
{
    [Area("Artists")]
    public class ArtistsController : Controller
    {
        private readonly IArtistsService _artistsService;
        private readonly ITattoosService _tattoosService;

        public ArtistsController(IArtistsService artistsService, ITattoosService tattoosService)
        {
            this._artistsService = artistsService;
            this._tattoosService = tattoosService;
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

            this.ViewData["TattooStyles"] = this._tattoosService.GetAllStyles()
                .Select(ts => new SelectListItem
                {
                    Value = ts.ToString(),
                    Text = ts.ToString()
                });
            return this.View(artist);
        }
    }
}