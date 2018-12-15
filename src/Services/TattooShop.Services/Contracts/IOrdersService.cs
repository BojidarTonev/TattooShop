using System.Threading.Tasks;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IOrdersService
    {
        Task<bool> AddOrder(string address, string description, int quantity, Product product, TattooShopUser user, string userId);
    }
}
