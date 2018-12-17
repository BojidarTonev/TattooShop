using TattooShop.Data.Models.Contracts;
using TattooShop.Data.Models.Enums;

namespace TattooShop.Data.Models
{
    public class Product : BaseModel<string>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}
