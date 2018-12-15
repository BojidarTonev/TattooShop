using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TattooShop.Web.Models
{
    public class AboutArtistsDisplayViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string TattoosDone { get; set; }

        public string Autobiography { get; set; }

        public string ImageUrl { get; set; }
    }
}
