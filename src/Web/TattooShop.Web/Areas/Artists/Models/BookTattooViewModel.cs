using Microsoft.AspNetCore.Http;
using TattooShop.Data.Models;
using TattooShop.Data.Models.Enums;

namespace TattooShop.Web.Areas.Artists.Models
{
    public class BookTattooInputViewModel
    {
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public string BookedFor { get; set; }

        public string BookedOn { get; set; }

        public string ArtistId { get; set; }
        public Artist Artist { get; set; }

        public string UserId { get; set; }
        public TattooShopUser User { get; set; }

        public TattooStyles Style { get; set; }
    }
}
