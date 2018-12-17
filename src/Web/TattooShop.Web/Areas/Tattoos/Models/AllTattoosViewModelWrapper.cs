using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TattooShop.Web.Areas.Tattoos.Models
{
    public class AllTattoosViewModelWrapper
    {
        public List<AllTattoosViewModel> Tattoos { get; set; }

        public string DisplayStyle { get; set; }
    }
}
