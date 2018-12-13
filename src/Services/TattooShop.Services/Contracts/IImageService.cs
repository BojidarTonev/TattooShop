using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TattooShop.Services.Contracts
{
    public interface IImageService
    {
        string AddToCloudinaryAndReturnImageUrl(IFormFile formFile);

        Task<bool> SaveAllAsync();
    }
}
