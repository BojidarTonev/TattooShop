using System;
using TattooShop.Data.Models.Contracts;
using TattooShop.Data.Models.Enums;

namespace TattooShop.Data.Models
{
    public class Tattoo : BaseModel<string>
    {
        public DateTime DoneOn { get; set; }

        public string ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        public string TattooRelevantName { get; set; }

        public Style TattooStyle { get; set; }

        public string TattooUrl { get; set; }

        public int Sessions { get; set; }
    }
}
