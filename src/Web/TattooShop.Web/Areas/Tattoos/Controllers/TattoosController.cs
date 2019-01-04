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
            var tattoo = this._tattoosService.Details<TattooDetailsViewModel>(id);

            var similarTattooDtos = this._tattoosService.OtherSimilar(tattoo.TattooStyle)
                .To<SimilarTattooViewModel>()
                .ToList();

            tattoo.SimilarTattoos = similarTattooDtos;

            return this.View(tattoo);
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