using Microsoft.AspNetCore.Http;

namespace TattooShop.Web.Areas.Artists.Models
{
    public class BookTattooInputViewModel
    {
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public string BookedFor { get; set; }

        public string Style { get; set; }

        public string ArtistImageUrl { get; set; }
    }
}
