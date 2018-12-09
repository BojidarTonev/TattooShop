﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TattooShop.Data.Models
{
    public class TattooShopUser : IdentityUser
    {
        public override string UserName { get; set; }

        public override string Email { get; set; }

        public override string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Order> Orders => new List<Order>();

        public virtual ICollection<Book> Books => new List<Book>();
    }
}
