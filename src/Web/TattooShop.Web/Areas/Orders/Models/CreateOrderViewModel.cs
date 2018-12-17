﻿using System.ComponentModel.DataAnnotations;

namespace TattooShop.Web.Areas.Orders.Models
{
    public class CreateOrderViewModel
    {
        [Required]
        public string Description { get; set; }

        public int Quantity { get; set; }

        public string DeliveryAddress { get; set; }

        public string UserAddress { get; set; }

        public OrderProductDisplayViewModel Product { get; set; }
    }
}
