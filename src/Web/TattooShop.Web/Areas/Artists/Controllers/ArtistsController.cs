﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using TattooShop.Services.Automapper;
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
                .To<DisplayAllArtistsViewModel>()
                .ToList();

            return this.View(artists);
        }

        public IActionResult Details(string id)
        {
            var artist = this._artistsService.Details<DisplayArtistDetailsViewModel>(id);
            var artistTattoos = this._artistsService.GetArtistsArtwork<ArtistDetailsTattoosViewModel>(artist.Id);

            artist.Tattoos = artistTattoos.ToList();

            return View(artist);
        }

        [Authorize]
        public IActionResult BookTattoo(string id)
        {
            var artist = this._artistsService.Details<BookTattooInputViewModel>(id);

            this.ViewData["TattooStyles"] = this._tattoosService.GetAllStyles()
                .Select(ts => new SelectListItem
                {
                    Value = ts.Id.ToString(),
                    Text = ts.Name.ToString()
                });

            return this.View(artist);
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

            var style = this._stylesService.GetStyle(model.Style).Name.ToString();

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookSuccessful = this._artistsService.AddBook(model.BookedFor, model.Description, model.Image, style, userId, artistId).Result;

            if (!bookSuccessful)
            {
                return this.View("Error");
            }

            var artistsDto = this._artistsService.All()
                .To<DisplayAllArtistsViewModel>()
                .ToList();

            return this.View("All", artistsDto);
        }
    }
}