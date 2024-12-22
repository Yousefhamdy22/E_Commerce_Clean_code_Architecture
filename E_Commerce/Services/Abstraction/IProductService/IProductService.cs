using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Helpers;

namespace E_Commerce.Services.Abstraction.ProductService
{
    public interface IProductService
    {
        public Task<string> CreateProduct(Product product);
        IEnumerable<Product> SearchProducts(string searchTerm);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        public IQueryable<Product> GetAllProductsQueryable();
        public Task<Product> GetByIdAsync(int id);
        public Task<List<Product>> GetByName(string name);
        public IQueryable<Product> FilterProductPaginatedQuerable(ProductOrderingEnum orderingEnum, string Search);
        public Task<List<Product>> GetAllBySortReviewAsync();
        public Task<List<Product>> GetProductListAsync();

        //Task<IEnumerable<Product>> GetAllProducts();


    }
}
