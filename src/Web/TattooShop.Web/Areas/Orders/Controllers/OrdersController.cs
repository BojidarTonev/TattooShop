using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;
using TattooShop.Web.Areas.Orders.Models;

namespace TattooShop.Web.Areas.Orders.Controllers
{
    [Area("Orders")]
    public class OrdersController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly UserManager<TattooShopUser> _userManager;
        private readonly IOrdersService _ordersService;
        private readonly IUsersService _usersService;

        public OrdersController(IProductsService productsService, UserManager<TattooShopUser> userManager, IOrdersService ordersService, IUsersService usersService)
        {
            this._productsService = productsService;
            this._userManager = userManager;
            this._ordersService = ordersService;
            this._usersService = usersService;
        }

        public IActionResult Details(string id)
        {
            var product = this._productsService.ProductDetails(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userAddress = this._usersService.GetUserAddress(userId);

            var productDto = new OrderProductDisplayViewModel()
            {
                Name = product.Name,
                Category = product.Category.ToString(),
                Description = product.Description,
                Price = product.Price.ToString(),
                ImageUrl = product.ImageUrl
            };
            var model = new CreateOrderViewModel()
            {
                Product = productDto,
                UserAddress = userAddress
            };

            return this.View(model);
        }

        public IActionResult FinishOrder(FinishOrderViewModel model)
        {
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Details(CreateOrderViewModel model)
        {
            var productId = this.HttpContext.GetRouteData().Values["id"].ToString();
            var product = this._productsService.ProductDetails(productId);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userAddress = this._usersService.GetUserAddress(userId);

            var orderSuccessful = this._ordersService.AddOrder(model.DeliveryAddress, model.Description, model.Quantity, productId, userId, null).Result;

            if (!orderSuccessful)
            {
                return this.View("Error");
            }

            var finalPrice = model.Quantity * product.Price;
            var finish = new FinishOrderViewModel()
            {
                FinalPrice = finalPrice,
                DeliveryAddress = model.DeliveryAddress,
                DeliveryDay = DateTime.UtcNow.ToString(),
                ProductName = product.Name
            };

            return this.View("FinishOrder", finish);
        }

    }
}