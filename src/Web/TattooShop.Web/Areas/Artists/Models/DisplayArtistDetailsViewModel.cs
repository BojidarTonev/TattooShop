using System.Collections.Generic;

namespace TattooShop.Web.Areas.Artists.Models
{
    public class DisplayArtistDetailsViewModel
    {
        public string ImageUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BestAt { get; set; }

        public string Autobiography { get; set; }

        public string ArtistId { get; set; }

        public int TattoosDone => this.Tattoos.Count;

        public ICollection<ArtistDetailsTattoosViewModel> Tattoos { get; set; }
    }
}
