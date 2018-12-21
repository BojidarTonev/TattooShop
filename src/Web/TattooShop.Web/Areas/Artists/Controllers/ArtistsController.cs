using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IStylesService _stylesService;

        public ArtistsController(IArtistsService artistsService, ITattoosService tattoosService, IStylesService stylesService)
        {
            this._artistsService = artistsService;
            this._tattoosService = tattoosService;
            this._stylesService = stylesService;
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
                    TattooStyle = tattoo.TattooStyle.Name.ToString(),
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

        [Authorize]
        public IActionResult BookTattoo(string id)
        {
            var artist = this._artistsService.Details(id);

            this.ViewData["TattooStyles"] = this._tattoosService.GetAllStyles()
                .Select(ts => new SelectListItem
                {
                    Value = ts.Id.ToString(),
                    Text = ts.Name.ToString()
                });

            var model = new BookTattooInputViewModel()
            {
                ArtistImageUrl = artist.ImageUrl
            };
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult BookTattoo(BookTattooInputViewModel model)
        {
            if (!TryValidateModel(model))
            {
                return this.View(model);
            }
            var artistId = this.HttpContext.GetRouteData().Values["id"].ToString();
            var artist = this._artistsService.Details(artistId);

            var style = this._stylesService.GetStyle(model.Style).Name.ToString();

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookSuccessful = this._artistsService.AddBook(model.BookedFor, model.Description, model.Image, style, userId, artist).Result;

            if (!bookSuccessful)
            {
                return this.View("Error");
            }

            var artistsDto = this._artistsService.All()
                .Select(a => new DisplayAllArtistsViewModel()
                {
                    ArtistId = a.Id,
                    Autobiography = a.Autobiography,
                    BestAt = a.BestAt.ToString(),
                    FirstName = a.FirstName,
                    ImageUrl = a.ImageUrl,
                    LastName = a.LastName
                });

            return this.View("All", artistsDto);
        }
    }
}