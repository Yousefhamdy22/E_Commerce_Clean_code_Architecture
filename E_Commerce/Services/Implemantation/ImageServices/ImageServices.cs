using E_Commerce.Domain.Entities;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Services.Abstraction.IImageServices;
using E_Commerce.Services.Implemantation.ImageServices;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services.Implemantation.ImageServices
{
    public class ImageServices : IImageServices
    {
        #region Fields
        private readonly IImageRepo _imageRepository;
        #endregion


        #region Constructor
        public ImageServices(IImageRepo imageRepository)
        {
            _imageRepository = imageRepository;
        }
        #endregion


        #region HandleFunctions


        public async Task<List<ProductImages>> GetProductImagesByProductIdAsync(int productId)
        {
            // Retrieve images for the specified product from the database
            var images = await _imageRepository.GetTableNoTracking()
                .Where(img => img.ProductId == productId)
                .ToListAsync();
            return images;
        }

        #endregion
    }
}
