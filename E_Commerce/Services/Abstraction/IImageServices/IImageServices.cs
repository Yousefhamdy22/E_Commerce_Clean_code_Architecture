using E_Commerce.Domain.Entities;

namespace E_Commerce.Services.Abstraction.IImageServices
{
    public interface IImageServices
    {
        public Task<List<ProductImages>> GetProductImagesByProductIdAsync(int id);
    }
}
