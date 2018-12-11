using Microsoft.AspNetCore.Mvc;
using TattooShop.Services.Contracts;

namespace TattooShop.Web.Areas.Products.Controllers
{
    [Area("Products")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            this._productsService = productsService;
        }

        public IActionResult All()
        {
            var products = this._productsService.All();

            return View(products);
        }

        public IActionResult Details(string id)
        {
            var product = this._productsService.ProductDetails(id);
            return this.View(product);
        }

        public IActionResult AllCategorized(string category)
        {
            var products = this._productsService.ProductsByCategory(category);

            return this.View("All", products);
        }

        [HttpPost]
        public IActionResult All(string category)
        {
            var products = this._productsService.ProductsByCategory(category);

            return this.View("All", products);
        }
    }
}