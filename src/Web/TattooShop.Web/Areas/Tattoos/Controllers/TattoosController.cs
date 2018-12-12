using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TattooShop.Data.Models.Enums;
using TattooShop.Services.Contracts;

namespace TattooShop.Web.Areas.Tattoos.Controllers
{
    [Area("Tattoos")]
    public class TattoosController : Controller
    {
        private readonly ITattoosService _tattoosService;

        public TattoosController(ITattoosService tattoosService)
        {
            this._tattoosService = tattoosService;
        }

        public IActionResult All()
        {
            var tattoos = this._tattoosService.All();
            var tattooStyles = this._tattoosService.GetAllStyles().Select(t => new SelectListItem()
            {
                Value = t.ToString(),
                Text = t.ToString()
            });

            this.ViewData["TattooStyles"] = tattooStyles;
            return View(tattoos);
        }

        public IActionResult Details(string id)
        {
            var tattoo = this._tattoosService.Details(id);

            return this.View(tattoo);
        }
    }
}