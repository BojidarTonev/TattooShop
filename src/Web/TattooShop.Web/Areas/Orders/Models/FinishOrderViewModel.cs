namespace TattooShop.Web.Areas.Orders.Models
{
    public class FinishOrderViewModel
    {
        public decimal FinalPrice { get; set; }

        public string ProductName { get; set; }

        public string DeliveryAddress { get; set; }

        public string DeliveryDay { get; set; }
    }
}
