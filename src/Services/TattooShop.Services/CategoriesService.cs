using System.Linq;
using TattooShop.Data.Contracts;
using TattooShop.Data.Models;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> _categoriesRepository;

        public CategoriesService(IRepository<Category> categoriesRepository)
        {
            this._categoriesRepository = categoriesRepository;
        }

        public Category GetCategory(string id)
        {
            return this._categoriesRepository.All().FirstOrDefault(c => c.Id == id);
        }
    }
}
