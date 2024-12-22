using DashBoard.Application.DTOs;
using DashBoard.Core.Entities;
using E_Commerce.Domain.Entities;
using Shared.Dtos;

namespace DashBoard.Services.Abstractions
{
    public interface IDashboardProductService
    {

        Task<string> CreateProduct(DashboardProductDto DashproductDto);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<ProductDto>> GetProductsByNameAsync(string name);
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
