using System;
using System.Collections.Generic;
using System.Linq;
using TattooShop.Data.Models.Contracts;
using TattooShop.Data.Models.Enums;

namespace TattooShop.Data.Models
{
    public class Artist : BaseModel<string>
    {
        public Artist()
        {
            this.TattooCollection = new List<Tattoo>();
            this.Books = new List<Book>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Autobiography { get; set; }

        public DateTime BirthDate { get; set; }

        public int? TattoosDone => this.TattooCollection.Count();

        public string ImageUrl { get; set; }

        public TattooStyles BestAt { get; set; }

        public virtual ICollection<Tattoo> TattooCollection { get; }

        public virtual ICollection<Book> Books { get; }
    }

}