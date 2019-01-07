using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TattooShop.Services.Contracts;
using TattooShop.Web.Areas.Orders.Models;
using TattooShop.Web.Areas.Products.Models;

namespace TattooShop.Web.Areas.Orders.Controllers
{
    [Area("Orders")]
    public class OrdersController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IOrdersService _ordersService;
        private readonly IUsersService _usersService;

        public OrdersController(IProductsService productsService, 
            IOrdersService ordersService, 
            IUsersService usersService)
        {
            this._productsService = productsService;
            this._ordersService = ordersService;
            this._usersService = usersService;
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var product = this._productsService.ProductDetails<OrderProductDisplayViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userAddress = this._usersService.GetUserAddress(userId);

            var dto = new CreateOrderViewModel()
            {
                Product = product,
                UserAddress = userAddress
            };

            return this.View(dto);
        }

        [Authorize]
        public IActionResult FinishOrder(FinishOrderViewModel model)
        {
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Details(CreateOrderViewModel model)
        {
            if (!TryValidateModel(model))
            {
                var productt = this._productsService.ProductDetails<OrderProductDisplayViewModel>(model.Id);
                var userIdd = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userAddress = this._usersService.GetUserAddress(userIdd);

                var dto = new CreateOrderViewModel()
                {
                    Product = productt,
                    UserAddress = userAddress
                };

                return this.View(dto);
            }
            var productId = this.HttpContext.GetRouteData().Values["id"].ToString();
            var product = this._productsService.ProductDetails<ProductDetailsViewModel>(productId);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderSuccessful = this._ordersService.AddOrder(model.DeliveryAddress, model.Description, model.Quantity, productId, userId).Result;

            if (!orderSuccessful)
            {
                var productt = this._productsService.ProductDetails<OrderProductDisplayViewModel>(model.Id);
                var userIdd = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userAddress = this._usersService.GetUserAddress(userIdd);

                var dto = new CreateOrderViewModel()
                {
                    Product = productt,
                    UserAddress = userAddress
                };

                return this.View(dto);
            }

            var finalPrice = model.Quantity * decimal.Parse(product.Price);

            var finish = new FinishOrderViewModel()
            {
                FinalPrice = finalPrice,
                DeliveryAddress = model.DeliveryAddress,
                DeliveryDay = DateTime.UtcNow.AddDays(3).ToString("D"),
                ProductName = product.Name
            };

            return this.View("FinishOrder", finish);
        }

    }
}