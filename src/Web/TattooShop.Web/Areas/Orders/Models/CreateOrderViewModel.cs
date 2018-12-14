using System;
using TattooShop.Data.Models;

namespace TattooShop.Web.Areas.Orders.Models
{
    public class CreateOrderViewModel
    {
        public string Description { get; set; }

        public string Category { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string UserId { get; set; }
        public virtual TattooShopUser User { get; set; }

        public string OrderedOn { get; set; }

        public DateTime EstimatedDeliveryDay { get; set; }

        public int Quantity { get; set; }

        public string DeliveryAddress { get; set; }
    }
}
