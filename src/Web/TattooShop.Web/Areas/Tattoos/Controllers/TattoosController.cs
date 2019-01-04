using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TattooShop.Services.Automapper;
using TattooShop.Services.Contracts;
using TattooShop.Web.Areas.Tattoos.Models;

namespace TattooShop.Web.Areas.Tattoos.Controllers
{
    [Area("Tattoos")]
    public class TattoosController : Controller
    {
        private readonly ITattoosService _tattoosService;
        private readonly IArtistsService _artistsService;
        private readonly IStylesService _stylesService;

        public TattoosController(ITattoosService tattoosService, IArtistsService artistsService, IStylesService stylesService)
        {
            this._tattoosService = tattoosService;
            this._artistsService = artistsService;
            this._stylesService = stylesService;
        }

        public IActionResult All()
        {
            var tattoos = this._tattoosService.All()
                .To<AllTattoosViewModel>()
                .ToList();

            var tattooStyles = this._tattoosService.GetAllStyles().Select(t => new SelectListItem()
            {
                Value = t.Id,
                Text = t.Name.ToString()
            });

            this.ViewData["TattooStyles"] = tattooStyles;

            var dto = new AllTattoosViewModelWrapper()
            {
                DisplayStyle = "All",
                Tattoos = tattoos
            };

            return View(dto);
        }

        public IActionResult Details(string id)
        {
            var tattoo = this._tattoosService.Details(id);
            var artist = this._artistsService.Details(tattoo.ArtistId);

            var similarTattooDtos = this._tattoosService.OtherSimilar(tattoo.TattooStyle.Name)
                .Select(t => new SimilarTattooViewModel()
                {
                    Id = t.Id,
                    RelevantName = t.TattoRelevantName,
                    TattooStyle = t.TattooStyle.Name.ToString(),
                    TattooUrl = t.TattooUrl
                }).ToList();

            var artistDto = new TattooArtistViewModel()
            {
                FirstName = artist.FirstName,
                Id = artist.Id,
                LastName = artist.LastName
            };

            var tattooDto = new TattooDetailsViewModel()
            {
                Artist = artistDto,
                DoneOn = tattoo.DoneOn.ToString(),
                Sessions = tattoo.Sessions.ToString(),
                SimilarTattoos = similarTattooDtos,
                TattooRelevantName = tattoo.TattoRelevantName,
                TattooUrl = tattoo.TattooUrl
            };

            return this.View(tattooDto);
        }

        [HttpPost]
        public IActionResult All(AllTattoosViewModelWrapper model)
        {
            var styleId = model.DisplayStyle;
            var style = this._stylesService.GetStyle(styleId);

            var tattoos = this._tattoosService.GetAllTattoosFromStyle(style.Name.ToString())
                .To<AllTattoosViewModel>()
                .ToList();

            var tattooStyles = this._tattoosService.GetAllStyles().Select(t => new SelectListItem()
            {
                Value = t.Id,
                Text = t.Name.ToString()
            });

            this.ViewData["TattooStyles"] = tattooStyles;

            var dto = new AllTattoosViewModelWrapper()
            {
                DisplayStyle = "All",
                Tattoos = tattoos
            };

            return View(dto);
        }
    }
}