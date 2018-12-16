using System.Threading.Tasks;
using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IOrdersService
    {
        Task<bool> AddOrder(string address, string description, int quantity, string productId, string userId, string userAddress);
    }
}
