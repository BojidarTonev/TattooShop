using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TattooShop.Services.Contracts;
using TattooShop.Web.Areas.Products.Models;

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
            var products = this._productsService.All()
                .Select(p => new ProductsAllViewModel()
                {
                    Category = p.Category.Name.ToString(),
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name
                });

            this.ViewData["ProductsCategories"] = this._productsService.GetAllCategories()
                .Select(pc => new SelectListItem
            {
                Value = pc.ToString(),
                Text = pc.ToString()
            });

            return View(products);
        }

        public IActionResult Details(string id)
        {
            var product = this._productsService.ProductDetails(id);
            var similars = this._productsService.OtherSimilar(product.Category.Name)
                .Select(p => new SimilarProductsDisplayModel()
                {
                    Category = p.Category.Name.ToString(),
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name
                }).ToList();

            var dto = new ProductDetailsViewModel()
            {
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price.ToString(),
                Category = product.Category.ToString(),
                ImageUrl = product.ImageUrl,
                SimilarProducts = similars
            };

            return this.View(dto);
        }
    }
}