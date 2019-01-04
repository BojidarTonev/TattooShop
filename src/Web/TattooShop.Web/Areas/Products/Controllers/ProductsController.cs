using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TattooShop.Services.Automapper;
using TattooShop.Services.Contracts;
using TattooShop.Web.Areas.Products.Models;

namespace TattooShop.Web.Areas.Products.Controllers
{
    [Area("Products")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            this._productsService = productsService;
            this._categoriesService = categoriesService;
        }

        public IActionResult All()
        {
            var products = this._productsService.All()
                .To<ProductsAllViewModel>()
                .ToList();

            var dto = new ProductsAllViewModelWrapper()
            {
                Products = products,
                DisplayCategory = "All"
            };

            var productsCategories = this._productsService.GetAllCategories().Select(t => new SelectListItem()
            {
                Value = t.Id,
                Text = t.Name.ToString()
            });

            this.ViewData["ProductsCategories"] = productsCategories;

            return View(dto);
        }

        [HttpPost]
        public IActionResult All(ProductsAllViewModelWrapper model)
        {
            var categoryId = model.DisplayCategory;
            var category = this._categoriesService.GetCategory(categoryId);

            var products = this._productsService.GetAllProductsByCategory(category.Name.ToString())
                .To<ProductsAllViewModel>()
                .ToList();

            var productsCategories = this._productsService.GetAllCategories().Select(t => new SelectListItem()
            {
                Value = t.Id,
                Text = t.Name.ToString()
            });

            this.ViewData["ProductsCategories"] = productsCategories;

            var dto = new ProductsAllViewModelWrapper()
            {
                DisplayCategory = category.Name.ToString(),
                Products = products
            };

            return View(dto);
        }

        public IActionResult Details(string id)
        {
            var product = this._productsService.ProductDetails(id);
            var similars = this._productsService.OtherSimilar(product.Category.Name)
                .To<SimilarProductsDisplayModel>()
                .ToList();

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