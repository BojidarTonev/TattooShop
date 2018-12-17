using System;
using System.Collections.Generic;
using System.Globalization;
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
            SeedTattooShopDatabaseRolesAndDefaultAdminAndArtist(serviceProvider);
            SeedEnumsDatabase(serviceProvider);
            SeedSampleProductsData(serviceProvider);
            SeedSampleTattooArtistData(serviceProvider);
            SeedSampleTattoosData(serviceProvider);
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
            int numberOfArtistsToSeed = 3;
            var db = serviceProvider.GetService<TattooShopContext>();
            if (!db.Artists.Any())
            {
                var artistsToAdd = new List<Artist>();
                string firstArtistImage =
                    "http://opentranscripts.org/wp-content/uploads/2015/12/filip-leu.png";
                string secondArtistImage =
                    "https://www.tattoolife.com/wp-content/uploads/2017/05/tattoolife-tattoo-artists-section.jpg";
                string thirdArtistImage =
                    "https://vice-images.vice.com/images/content-images-crops/2015/08/11/sampa-tattoo-all-girl-parlour-876brazil-body-image-1439305125-size_1000.jpg?output-quality=75?resize=320:*";
                for (int i = 0; i < numberOfArtistsToSeed; i++)
                {
                    string url;
                    TattooStyles bestAt;
                    if (i == 0)
                    {
                        url = firstArtistImage;
                        bestAt = TattooStyles.Realistic;
                    }
                    else if (i == 1)
                    {
                        url = secondArtistImage;
                        bestAt = TattooStyles.Geometric;
                    }
                    else
                    {
                        url = thirdArtistImage;
                        bestAt = TattooStyles.TraditionalJapanese;
                    }

                    var artist = new Artist()
                    {
                        Autobiography = $"Bla-bla-bla-bla I am great artist <3. I have done {i} tattoos by now.",
                        BestAt = bestAt,
                        BirthDate = DateTime.ParseExact($"11/08/199{i}", "dd/mm/yyyy", CultureInfo.InvariantCulture),
                        FirstName = $"Peter",
                        LastName = $"01{i}",
                        ImageUrl = url
                    };

                    artistsToAdd.Add(artist);
                }

                db.Artists.AddRange(artistsToAdd);
                db.SaveChanges();
            }
        }

        public static void SeedSampleProductsData(IServiceProvider serviceProvider)
        {
            const int numberOfItemsToSeed = 10;

            const string TattoCareCategory = "TattoCare";
            const string TattoCareImageUrl = "https://i.etsystatic.com/14397240/r/il/034b89/1339559018/il_570xN.1339559018_hbcs.jpg";

            const string PiercingAndSouvenirCategory = "Piercing and souvenir";
            const string PiercingAndSouvenirImageUrl = "http://www.cuded.com/wp-content/uploads/2014/04/20-Ear-Piercings.jpg";

            const string ClothesCategory = "Clothes";
            const string ClothesImageUrl = "https://cdn.shopify.com/s/files/1/1248/7893/products/employed_242471d7-7144-4bff-abff-ec26d7d1ab5a_900x.jpg?v=1531421487";

            var db = serviceProvider.GetService<TattooShopContext>();

            if (!db.Products.Any())
            {
                var productsToAdd = new List<Product>();
                for (int i = 0; i < numberOfItemsToSeed; i++)
                {
                    if (i <= 3)
                    {
                        var product = new Product()
                        {
                            Name = $"Tattoo care product{i}",
                            Category = new Category()
                            {
                                Name = ProductsCategories.TattooCare
                            },
                            Description = $"This is a great product buy it now please",
                            Price = decimal.Parse("22.50"),
                            ImageUrl = TattoCareImageUrl
                        };

                        productsToAdd.Add(product);
                    }
                    else if (i <= 6)
                    {
                        var product = new Product()
                        {
                            Name = $"Piercing product {i}",
                            Category = new Category()
                            {
                                Name = ProductsCategories.Piercing
                            },
                            Description = $"This is a great product buy it now please",
                            Price = decimal.Parse("22.50"),
                            ImageUrl = PiercingAndSouvenirImageUrl
                        };

                        productsToAdd.Add(product);
                    }
                    else
                    {
                        var product = new Product()
                        {
                            Name = $"Employed t-shirt {i}",
                            Category = new Category()
                            {
                                Name = ProductsCategories.Clothes
                            },
                            Description = $"This is a great product buy it now please",
                            Price = decimal.Parse("22.50"),
                            ImageUrl = ClothesImageUrl
                        };

                        productsToAdd.Add(product);
                    }
                }

                db.Products.AddRange(productsToAdd);
                db.SaveChanges();
            }
        }

        public static void SeedSampleTattoosData(IServiceProvider serviceProvider)
        {
            const int NumberTattoosToSeed = 15;
            const string firstTattooUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQoQE1fzbgF-DKuG8y_FLyD1mE2S9V8a-T7geIy6Y-PXXs78uva";
            const string secondTattooUrl = "https://i.pinimg.com/originals/24/b4/ac/24b4ac1a18eed9379d231bc06bb6a3a7.jpg";
            const string thirdTattooUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS5YdjOBLTmfr2ctsacFE4S6nwJtz9do_SFRycTJT_9lrnT-D0T-Q";

            var db = serviceProvider.GetService<TattooShopContext>();

            var americanTraditionalStyle = db.Styles.First(s => s.Name == TattooStyles.AmericanTraditional);
            var geometricStyle = db.Styles.First(s => s.Name == TattooStyles.Geometric);
            var realisticStyle = db.Styles.First(s => s.Name == TattooStyles.Realistic);

            if (!db.Tattoos.Any())
            {
                var tattooList = new List<Tattoo>();

                for (int i = 0; i < NumberTattoosToSeed; i++)
                {
                    if (i % 2 == 0)
                    {
                        var tattoo = new Tattoo()
                        {
                            Artist = db.Artists.First(),
                            DoneOn = DateTime.UtcNow.Date,
                            Sessions = i,
                            TattooStyle = americanTraditionalStyle,
                            TattooUrl = firstTattooUrl,
                            TattoRelevantName = $"{i} Petko"
                        };

                        db.Tattoos.Add(tattoo);
                        db.SaveChanges();
                    }
                    else if (i % 3 == 0)
                    {
                        var tattoo = new Tattoo()
                        {
                            Artist = db.Artists.Skip(1).First(),
                            DoneOn = DateTime.UtcNow.Date,
                            Sessions = i,
                            TattooStyle = geometricStyle,
                            TattooUrl = secondTattooUrl,
                            TattoRelevantName = $"{i} Bojidar"
                        };

                        db.Tattoos.Add(tattoo);
                        db.SaveChanges();
                    }
                    else
                    {
                        var tattoo = new Tattoo()
                        {
                            Artist = db.Artists.Skip(2).First(),
                            DoneOn = DateTime.UtcNow.Date,
                            Sessions = i,
                            TattooStyle = realisticStyle,
                            TattooUrl = thirdTattooUrl,
                            TattoRelevantName = $"{i} Kristiqn"
                        };

                        db.Tattoos.Add(tattoo);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}