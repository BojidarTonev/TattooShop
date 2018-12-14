using System;
using TattooShop.Data.Models.Contracts;

namespace TattooShop.Data.Models
{
    public class Order : BaseModel<string>
    {
        public string UserId { get; set; }
        public virtual TattooShopUser User { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public DateTime OrderedOn { get; set; }

        public DateTime EstimatedDeliveryDay { get; set; }

        public int Quantity { get; set; }

        public string DeliveryAddress { get; set; }

        public string Description { get; set; }
    }
}
