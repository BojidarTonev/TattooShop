using System;
using TattooShop.Data.Models.Contracts;
using TattooShop.Data.Models.Enums;

namespace TattooShop.Data.Models
{
    public class Book : BaseModel<string>
    {
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime BookedFor { get; set; }

        public DateTime BookedOn { get; set; }

        public TattooStyles TattooStyle { get; set; }

        public string ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        public string UserId { get; set; }
        public TattooShopUser User { get; set; }
    }
}