using DashBoard.Core.Entities;
using DashBoard.Infrastructure.APIClients.Abstractions;
using DashBoard.Infrastructure.Repositories;
using DashBoard.Infrastructure.Services.Abstractions;
using E_Commerce.Domain.Entities;
using E_Commerce.Services.Abstraction.ICategoryServices;

namespace DashBoard.Services.Implementations
{
    public class DashCategoryServices : ICategoryServices
    {
        private readonly IApiClient _apiClient;
        private readonly IproductDashRepo _productDashRepo;
        private readonly IGenaricRepository<DashProduct> _genaricRepository;


        public DashCategoryServices(IApiClient apiClient,
             IproductDashRepo productDashRepo, IGenaricRepository<DashProduct> genaricRepository)
        {
            _apiClient = apiClient;
            _productDashRepo = productDashRepo;
            _genaricRepository = genaricRepository;
        }


        public Task<string> CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdWithIncludeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetByNameAsunc(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetCategorytListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
