using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using TattooShop.Data;
using TattooShop.Services.Contracts;

namespace TattooShop.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly TattooShopContext _context;

        public ImageService(TattooShopContext context)
        {
            this._context = context;
            Account account = new Account()
            {
                ApiKey = "853797844879441",
                ApiSecret = "DuCN3DcTbrk5ZugYrISrihzq_qg",
                Cloud = "site-stuff"
            };

            _cloudinary = new Cloudinary(account);
        }

        public string AddToCloudinaryAndReturnImageUrl(IFormFile photo)
        {
            var file = photo;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Folder = "/UserTattooUploads"
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var url = uploadResult.Uri.ToString();

            return url;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
