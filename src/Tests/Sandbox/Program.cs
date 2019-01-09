using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TattooShop.Data;
using TattooShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models.Enums;

namespace Sandbox
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;
                SandboxCode(serviceProvider);
            }
        }

        private static void SandboxCode(IServiceProvider serviceProvider)
        {
            //TODO: code here...
            SeedDatabase(serviceProvider);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddDbContext<TattooShopContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<TattooShopUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<TattooShopContext>();

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
        }

        private static void SeedDatabase(IServiceProvider serviceProvider)
        {
            SeedTattooShopDatabaseRolesAndDefaultAdminAndArtist(serviceProvider);
            SeedEnumsDatabase(serviceProvider);
            SeedSampleProductsData(serviceProvider);
            SeedSampleTattooArtistData(serviceProvider);
            SeedSampleTattoosData(serviceProvider);
        }

        private static void SeedEnumsDatabase(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<TattooShopContext>();
            if (!db.Styles.Any() && !db.Categories.Any())
            {
                var styles = new List<TattooStyles>()
                {
                    TattooStyles.AmericanTraditional,
                    TattooStyles.Biomechanical,
                    TattooStyles.Geometric,
                    TattooStyles.Polynesian,
                    TattooStyles.Realistic,
                    TattooStyles.TraditionalJapanese,
                    TattooStyles.Watercolor
                };
                var categories = new List<ProductsCategories>()
                {
                    ProductsCategories.Clothes,
                    ProductsCategories.Piercing,
                    ProductsCategories.TattooCare
                };

                var tattooStyles = new List<Style>();
                var productsCategories = new List<Category>();

                for (int i = 0; i < styles.Count; i++)
                {
                    Style style = new Style()
                    {
                        Name = styles[i]
                    };
                    tattooStyles.Add(style);
                }

                for (int i = 0; i < categories.Count; i++)
                {
                    Category category = new Category()
                    {
                        Name = categories[i]
                    };
                    productsCategories.Add(category);
                }

                db.Categories.AddRange(productsCategories);
                db.Styles.AddRange(tattooStyles);

                db.SaveChanges();

            }
        }

        public static void SeedTattooShopDatabaseRolesAndDefaultAdminAndArtist(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<TattooShopContext>();
            Console.WriteLine(db.Users.Count());

            if (!db.Roles.AnyAsync().Result)
            {
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    var adminRole = GlobalConstants.AdminRole;
                    var userRole = GlobalConstants.UserRole;
                    var artistRole = GlobalConstants.TattooArtistRole;

                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = adminRole
                    });

                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = userRole
                    });

                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = artistRole
                    });

                }).Wait();
            }

            if (!db.Users.AnyAsync().Result)
            {
                var userManager = serviceProvider.GetService<UserManager<TattooShopUser>>();

                Task.Run(async () =>
                {
                    var adminPasswrod = "admin123";
                    var admin = new TattooShopUser()
                    {
                        UserName = "Admin",
                        Email = "Admin@admin.com",
                        FirstName = "Admin",
                        LastName = "Adminov"
                    };

                    var artistPassword = "artist123";
                    var artist = new TattooShopUser()
                    {
                        UserName = "Artist",
                        Email = "Artist@artist.com",
                        FirstName = "Artist",
                        LastName = "Artistov"
                    };

                    await userManager.CreateAsync(admin, adminPasswrod);
                    await userManager.AddToRoleAsync(admin, GlobalConstants.AdminRole);

                    await userManager.CreateAsync(artist, artistPassword);
                    await userManager.AddToRoleAsync(artist, GlobalConstants.TattooArtistRole);

                }).Wait();


            }
        }

        public static void SeedSampleTattooArtistData(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<TattooShopContext>();

            if (!db.Artists.Any())
            {
                var artistsToAdd = CreateArtistsEntities(db);

                db.Artists.AddRange(artistsToAdd);
                db.SaveChanges();
            }
        }

        private static List<Artist> CreateArtistsEntities(TattooShopContext db)
        {
            var artists = new List<Artist>();

            string firstArtistImage =
                "http://opentranscripts.org/wp-content/uploads/2015/12/filip-leu.png";
            string secondArtistImage =
                "https://www.tattoolife.com/wp-content/uploads/2017/05/tattoolife-tattoo-artists-section.jpg";
            string thirdArtistImage =
                "https://vice-images.vice.com/images/content-images-crops/2015/08/11/sampa-tattoo-all-girl-parlour-876brazil-body-image-1439305125-size_1000.jpg?output-quality=75?resize=320:*";

            var firstArtistBestAt = db.Styles.First(s => s.Name == TattooStyles.Geometric).Name;
            var secondArtistBestAt = db.Styles.First(s => s.Name == TattooStyles.Realistic).Name;
            var thirdArtistBestAt = db.Styles.First(s => s.Name == TattooStyles.Polynesian).Name;

            var firstArtist = new Artist()
            {
                Autobiography = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                BestAt = firstArtistBestAt,
                BirthDate = DateTime.ParseExact("31-07-1988", "dd-MM-yyyy", null),
                FirstName = "Peter",
                LastName = "Peterson",
                ImageUrl = firstArtistImage
            };
            artists.Add(firstArtist);

            var secondArtist = new Artist()
            {
                Autobiography = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                BestAt = secondArtistBestAt,
                BirthDate = DateTime.ParseExact("05-12-1973", "dd-MM-yyyy", null),
                FirstName = "Storm",
                LastName = "Spirit",
                ImageUrl = secondArtistImage
            };
            artists.Add(secondArtist);

            var thirdArtist = new Artist()
            {
                Autobiography = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                BestAt = thirdArtistBestAt,
                BirthDate = DateTime.ParseExact("12-10-1987", "dd-MM-yyyy", null),
                FirstName = "Rubick",
                LastName = "Rubickson",
                ImageUrl = thirdArtistImage
            };
            artists.Add(thirdArtist);

            return artists;
        }

        public static void SeedSampleProductsData(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<TattooShopContext>();

            if (!db.Products.Any())
            {
                var productsToAdd = CreateProductsEntities(db);

                db.Products.AddRange(productsToAdd);
                db.SaveChanges();
            }
        }

        private static List<Product> CreateProductsEntities(TattooShopContext db)
        {
            const string tattooCareProductOneImageUrl = "https://i.etsystatic.com/14397240/r/il/034b89/1339559018/il_570xN.1339559018_hbcs.jpg";
            const string tattooCareProductTwoImageUrl = "https://www.electricinkskin.com/images/skincare_slide.jpg";
            const string piercingImageUrl = "https://authoritytattoo.com/wp-content/uploads/2017/09/Industrial-Piercing-3.jpg";
            const string piercingTwoImageUrl = "https://i.pinimg.com/originals/4a/e0/d9/4ae0d93483b04b3ccb6998c985e45e38.jpg";
            const string clothesImageUrl = "https://shop.r10s.jp/kuziyaku/cabinet/ts/kz-t-56/img57604367.jpg";
            const string clothesTwoImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR1mmHi-otjqNw5Ghat2cqsv8p2ZMpjtN4OfuiS6_xzGGu-sWeM";

            var clothesCategory = db.Categories.First(c => c.Name == ProductsCategories.Clothes);
            var piercingCategory = db.Categories.First(c => c.Name == ProductsCategories.Piercing);
            var tattooCareCategory = db.Categories.First(c => c.Name == ProductsCategories.TattooCare);

            var products = new List<Product>();

            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    var tattooCareProduct = new Product()
                    {
                        Category = tattooCareCategory,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                        ImageUrl = tattooCareProductOneImageUrl,
                        Name = "Ink balm 28g. 250ml",
                        Price = 25.99m
                    };
                    products.Add(tattooCareProduct);

                    var piercingProduct = new Product()
                    {
                        Category = piercingCategory,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                        ImageUrl = piercingImageUrl,
                        Name = "Piercing arrow",
                        Price = 14.99m
                    };
                    products.Add(piercingProduct);

                    var clothesProduct = new Product()
                    {
                        Category = clothesCategory,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                        ImageUrl = clothesImageUrl,
                        Name = "YX Japanese traditional black and white t-shirt",
                        Price = 25.99m
                    };
                    products.Add(clothesProduct);

                }
                else
                {
                    var tattooCareProduct = new Product()
                    {
                        Category = tattooCareCategory,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                        ImageUrl = tattooCareProductTwoImageUrl,
                        Name = "Electric ink 3.7fl. oz. 110ml",
                        Price = 29.99m
                    };
                    products.Add(tattooCareProduct);

                    var piercingProduct = new Product()
                    {
                        Category = piercingCategory,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                        ImageUrl = piercingTwoImageUrl,
                        Name = "Mouth piercing",
                        Price = 14.99m
                    };
                    products.Add(piercingProduct);

                    var clothesProduct = new Product()
                    {
                        Category = clothesCategory,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                        ImageUrl = clothesTwoImageUrl,
                        Name = "Just ink it black t-shirt",
                        Price = 30.99m
                    };
                    products.Add(clothesProduct);
                }
            }

            return products;
        }

        public static void SeedSampleTattoosData(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<TattooShopContext>();

            if (!db.Tattoos.Any())
            {
                var tattooList = CreateTattooEntities(db);

                db.Tattoos.AddRange(tattooList);
                db.SaveChanges();
            }
        }

        private static List<Tattoo> CreateTattooEntities(TattooShopContext db)
        {
            const string americanTraditionalTattooUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQoQE1fzbgF-DKuG8y_FLyD1mE2S9V8a-T7geIy6Y-PXXs78uva";
            const string americanTraditionalTwoTattooUrl = "http://stephenking.club/wp-content/uploads/2018/06/american-traditional-chest-piece-traditional-tattoo-sleeves-chest-american-traditional-owl-chest-tattoo.jpg";

            const string geometricTattooUrl = "https://i.pinimg.com/originals/24/b4/ac/24b4ac1a18eed9379d231bc06bb6a3a7.jpg";
            const string geometricTwoTattooUrl = "https://i.ytimg.com/vi/BK-o-pa8zUA/maxresdefault.jpg";

            const string realisticTattooUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQUbgv4hHk2IfsUqYYTffZy7PAUAhW0XklYSmQn6jPszWj12o72";
            const string realisticTwoTattooUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSKXQS9EtI1VyEepL-GxDJt_6Hd1BcfRjPvhMcQQY_LslcMuVlGHA";

            const string biomechanicalTattooUrl = "https://i.pinimg.com/originals/af/49/0a/af490a6c8534a43306cf62a944452693.jpg";
            const string biomechanicalTwoTattooUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRE_kKEbe43TqKGSnY7lS-vC0zxFgqlnK9xuLQTyBR0nTHgxHDOWg";

            const string polynesianTattooUrl = "https://i.pinimg.com/originals/e2/49/60/e24960b50a8e042744cb95a6063a84db.jpg";
            const string polynesianTwoTattooUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS41vZiTbCoP1JtMCXEYT-ACZ-1x8CBcQ4xmQFU_xdOekfUE3Zw";

            const string japaneseTraditionalTattooUrl = "https://editorial.designtaxi.com/editorial-images/news-GakkinTattoos060516/1-Blackwork-Japanese-Tattoo-Gakkin.jpg";
            const string japaneseTraditionalTwoTattooUrl = "https://i.redd.it/jwoopymjuj601.jpg";

            const string watercolorTattooUrl = "https://kickassthings.com/wp-content/uploads/2017/05/best-watercolor-tattoo-artists-Adrian-Bascur-17.jpg";
            const string watercolorTwoTattooUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTLtkhkZM2dlhysdfUQ2XLZbbBpF8vJWQmZUx770mkb_uB7e62rEA";

            var americanTraditionalStyle = db.Styles.First(s => s.Name == TattooStyles.AmericanTraditional);
            var geometricStyle = db.Styles.First(s => s.Name == TattooStyles.Geometric);
            var realisticStyle = db.Styles.First(s => s.Name == TattooStyles.Realistic);
            var biomechanicalStyle = db.Styles.First(s => s.Name == TattooStyles.Biomechanical);
            var polynesianStyle = db.Styles.First(s => s.Name == TattooStyles.Polynesian);
            var japaneseTraditionalStyle = db.Styles.First(s => s.Name == TattooStyles.TraditionalJapanese);
            var watercolorStyle = db.Styles.First(s => s.Name == TattooStyles.Watercolor);

            var tattooList = new List<Tattoo>();

            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    var americanTraditionalTattoo = new Tattoo()
                    {
                        TattooStyle = americanTraditionalStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Peter"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(45)),
                        Sessions = 3,
                        TattooRelevantName = "Mad gorilla",
                        TattooUrl = americanTraditionalTattooUrl
                    };
                    tattooList.Add(americanTraditionalTattoo);

                    var geometricTattoo = new Tattoo()
                    {
                        TattooStyle = geometricStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Rubick"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(37)),
                        Sessions = 2,
                        TattooRelevantName = "Cool flowers",
                        TattooUrl = geometricTattooUrl
                    };
                    tattooList.Add(geometricTattoo);

                    var realisticTattoo = new Tattoo()
                    {
                        TattooStyle = realisticStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Storm"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(26)),
                        Sessions = 3,
                        TattooRelevantName = "Cats are cool",
                        TattooUrl = realisticTattooUrl
                    };
                    tattooList.Add(realisticTattoo);

                    var biomechanicalTattoo = new Tattoo()
                    {
                        TattooStyle = biomechanicalStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Peter"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(40)),
                        Sessions = 2,
                        TattooRelevantName = "Engineering stuff inked",
                        TattooUrl = biomechanicalTattooUrl
                    };
                    tattooList.Add(biomechanicalTattoo);

                    var traditionalJapaneseTattoo = new Tattoo()
                    {
                        TattooStyle = japaneseTraditionalStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Storm"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(12)),
                        Sessions = 9,
                        TattooRelevantName = "Big traditional japanese tattoo",
                        TattooUrl = japaneseTraditionalTattooUrl
                    };
                    tattooList.Add(traditionalJapaneseTattoo);

                    var polynesianTattoo = new Tattoo()
                    {
                        TattooStyle = polynesianStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Rubick"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(15)),
                        Sessions = 3,
                        TattooRelevantName = "Chest and hand maori",
                        TattooUrl = polynesianTattooUrl
                    };
                    tattooList.Add(polynesianTattoo);

                    var watercolorTattoo = new Tattoo()
                    {
                        TattooStyle = watercolorStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Peter"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(7)),
                        Sessions = 1,
                        TattooRelevantName = "Fresh cats",
                        TattooUrl = watercolorTattooUrl
                    };
                    tattooList.Add(watercolorTattoo);
                }
                else
                {
                    var americanTraditionalTattoo = new Tattoo()
                    {
                        TattooStyle = americanTraditionalStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Rubick"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(12)),
                        Sessions = 4,
                        TattooRelevantName = "Crow on chest holding keys",
                        TattooUrl = americanTraditionalTwoTattooUrl
                    };
                    tattooList.Add(americanTraditionalTattoo);

                    var geometricTattoo = new Tattoo()
                    {
                        TattooStyle = geometricStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Peter"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(10)),
                        Sessions = 3,
                        TattooRelevantName = "geometric sleeve",
                        TattooUrl = geometricTwoTattooUrl
                    };
                    tattooList.Add(geometricTattoo);

                    var realisticTattoo = new Tattoo()
                    {
                        TattooStyle = realisticStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Rubick"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(21)),
                        Sessions = 2,
                        TattooRelevantName = "Viking and emptiness",
                        TattooUrl = realisticTwoTattooUrl
                    };
                    tattooList.Add(realisticTattoo);

                    var biomechanicalTattoo = new Tattoo()
                    {
                        TattooStyle = biomechanicalStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Storm"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(42)),
                        Sessions = 2,
                        TattooRelevantName = "Instrument under skin",
                        TattooUrl = biomechanicalTwoTattooUrl
                    };
                    tattooList.Add(biomechanicalTattoo);

                    var traditionalJapaneseTattoo = new Tattoo()
                    {
                        TattooStyle = japaneseTraditionalStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Rubick"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(8)),
                        Sessions = 1,
                        TattooRelevantName = "Japanese style sun and waves",
                        TattooUrl = japaneseTraditionalTwoTattooUrl
                    };
                    tattooList.Add(traditionalJapaneseTattoo);

                    var polynesianTattoo = new Tattoo()
                    {
                        TattooStyle = polynesianStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Peter"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(19)),
                        Sessions = 5,
                        TattooRelevantName = "Big maori upper back",
                        TattooUrl = polynesianTwoTattooUrl
                    };
                    tattooList.Add(polynesianTattoo);

                    var watercolorTattoo = new Tattoo()
                    {
                        TattooStyle = watercolorStyle,
                        Artist = db.Artists.First(a => a.FirstName == "Storm"),
                        DoneOn = DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
                        Sessions = 1,
                        TattooRelevantName = "Fresh wolf",
                        TattooUrl = watercolorTwoTattooUrl
                    };
                    tattooList.Add(watercolorTattoo);
                }
                
            }

            return tattooList;
        }
    }
}