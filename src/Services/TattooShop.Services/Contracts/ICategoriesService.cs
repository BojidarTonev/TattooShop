using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface ICategoriesService
    {
        Category GetCategory(string id);
    }
}
