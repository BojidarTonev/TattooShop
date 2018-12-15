using System.Collections.Generic;

namespace TattooShop.Web.Areas.Tattoos.Models
{
    public class TattooDetailsViewModel
    {
        public string TattooUrl { get; set; }

        public string TattooRelevantName { get; set; }

        public string DoneOn { get; set; }

        public string Sessions { get; set; }

        public ICollection<SimilarTattooViewModel> SimilarTattoos { get; set; }

        public TattooArtistViewModel Artist { get; set; }
    }
}
