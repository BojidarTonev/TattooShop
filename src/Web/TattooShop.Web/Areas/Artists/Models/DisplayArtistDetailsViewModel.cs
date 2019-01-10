using System.Collections.Generic;
using AutoMapper;
using TattooShop.Data.Models;
using TattooShop.Services.Automapper;

namespace TattooShop.Web.Areas.Artists.Models
{
    public class DisplayArtistDetailsViewModel : IMapFrom<Artist>
    {
        public DisplayArtistDetailsViewModel()
        {
            this.TattooCollection = new List<ArtistDetailsTattoosViewModel>();
        }

        public string ImageUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Autobiography { get; set; }

        public string Id { get; set; }

        public int TattoosDone => this.TattooCollection.Count;

        public ICollection<ArtistDetailsTattoosViewModel> TattooCollection { get; set; }
    }
}
