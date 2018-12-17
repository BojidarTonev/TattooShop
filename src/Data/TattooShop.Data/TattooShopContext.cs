using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TattooShop.Data.Models;

namespace TattooShop.Data
{
    public class TattooShopContext : IdentityDbContext<TattooShopUser>
    {
        public TattooShopContext(DbContextOptions<TattooShopContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Tattoo> Tattoos { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Style> Styles { get; set; }

        public DbSet<ContactInfo> Feedback { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
