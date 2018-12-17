using System;
using System.Collections.Generic;
using System.Text;
using TattooShop.Data.Models.Enums;

namespace TattooShop.Data.Models
{
    public class Style
    {
        public string Id { get; set; }

        public TattooStyles Name { get; set; }
    }
}
