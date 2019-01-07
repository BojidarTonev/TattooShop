using System.ComponentModel.DataAnnotations;

namespace TattooShop.Web.Areas.Orders.Models
{
    public class CreateOrderViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Any special shipping desires?")]
        public string Description { get; set; }
        
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Miracles may be possible, but predicting your address isn't.")]
        public string DeliveryAddress { get; set; }

        public string UserAddress { get; set; }

        public OrderProductDisplayViewModel Product { get; set; }
    }
}
