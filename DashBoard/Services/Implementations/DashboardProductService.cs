using AutoMapper;
using DashBoard.Application.DTOs;
using DashBoard.Core.Entities;
using DashBoard.Infrastructure.APIClients.Abstractions;
using DashBoard.Infrastructure.Repositories;
using DashBoard.Infrastructure.Services.Abstractions;
using DashBoard.Services.Abstractions;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace DashBoard.Services.Implementations
{
    public class DashboardProductService : IDashboardProductService
    {
        private readonly IApiClient _apiClient;
        private readonly IMapper _mapper;
        private readonly IproductDashRepo _productDashRepo;
        private readonly IGenaricRepository<Product> _genaricRepository;


        public DashboardProductService(IApiClient apiClient,
             IproductDashRepo productDashRepo, IMapper mapper , IGenaricRepository<Product> genaricRepository)
        {
            _apiClient = apiClient;
            _productDashRepo = productDashRepo;
            _genaricRepository = genaricRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateProduct(DashboardProductDto productDto)
        {
            var ProductDash = _mapper.Map<Product>(productDto);

            ProductDash.CreatedDate = DateTime.UtcNow;

            var createdProduct = await _genaricRepository.Add(ProductDash);

            await _genaricRepository.Save();

            return createdProduct.ProductId.ToString(); ;
        }

      

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _productDashRepo.GetTableNoTracking().ToListAsync();
            if (!products.Any())
                return null;

            return products;

        }

        public Task<ProductDto> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
            // use ClassDto as returned 
            //Return data using Repo or another 
            // as return --> Add dashboard-specific logic here auto map 

            // Another Solution .. use apiclient .. without REPO can use Old service(main)

        }

        public Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto>> GetProductsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
