using TattooShop.Data.Models;

namespace TattooShop.Services.Contracts
{
    public interface IStylesService
    {
        Style GetStyle(string id);
    }
}
