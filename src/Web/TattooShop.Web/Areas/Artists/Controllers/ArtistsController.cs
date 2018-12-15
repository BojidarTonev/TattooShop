using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;
using TattooShop.Web.Areas.Artists.Models;

namespace TattooShop.Web.Areas.Artists.Controllers
{
    [Area("Artists")]
    public class ArtistsController : Controller
    {
        private readonly IArtistsService _artistsService;
        private readonly ITattoosService _tattoosService;
        private readonly UserManager<TattooShopUser> _userManager;

        public ArtistsController(IArtistsService artistsService, ITattoosService tattoosService, UserManager<TattooShopUser> userManager)
        {
            this._artistsService = artistsService;
            this._tattoosService = tattoosService;
            this._userManager = userManager;
        }

        public IActionResult All()
        {
            var artists = this._artistsService.All()
                .Select(a => new DisplayAllArtistsViewModel()
                {
                    ArtistId = a.Id,
                    Autobiography = a.Autobiography,
                    BestAt = a.BestAt.ToString(),
                    FirstName = a.FirstName,
                    ImageUrl = a.ImageUrl,
                    LastName = a.LastName
                });

            return this.View(artists);
        }

        public IActionResult Details(string id)
        {
            var tattoosDto = new List<ArtistDetailsTattoosViewModel>();
            var artist = this._artistsService.Details(id);

            foreach (var tattoo in artist.TattooCollection)
            {
                var tatDto = new ArtistDetailsTattoosViewModel()
                {
                    TattooStyle = tattoo.TattooStyle.ToString(),
                    TattooId = tattoo.Id,
                    TattooRelevantName = tattoo.TattoRelevantName,
                    TattooUrl = tattoo.TattooUrl
                };


                tattoosDto.Add(tatDto);
            }

            var dto = new DisplayArtistDetailsViewModel()
            {
                ArtistId = artist.Id,
                Autobiography = artist.Autobiography,
                BestAt = artist.BestAt.ToString(),
                FirstName = artist.FirstName,
                ImageUrl = artist.ImageUrl,
                LastName = artist.LastName,
                Tattoos = tattoosDto
            };

            return View(dto);
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

            var model = new BookTattooInputViewModel()
            {
                Artist = artist
            };
            return this.View(model);
        }

        [HttpPost]
        public IActionResult BookTattoo(BookTattooInputViewModel model)
        {
            var artistId = this.HttpContext.GetRouteData().Values["id"].ToString();
            var artist = this._artistsService.Details(artistId);

            var artists = this._artistsService.All();
            var user = this._userManager.GetUserAsync(this.User).Result;

            var bookSuccessful = this._artistsService.AddBook(model.BookedFor, model.Description, model.Image, model.Style, user, artist).Result;

            if (!bookSuccessful)
            {
                return this.View("Error");
            }

            return this.View("All", artists);
        }
    }
}