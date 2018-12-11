using Microsoft.AspNetCore.Mvc;
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

            return View(tattoos);
        }

        public IActionResult Details(string id)
        {
            var tattoo = this._tattoosService.Details(id);

            return this.View(tattoo);
        }
    }
}