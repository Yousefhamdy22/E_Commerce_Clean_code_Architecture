using E_Commerce.Domain.Entities;

namespace E_Commerce.Services.Abstraction.ICategoryServices
{
    public interface ICategoryServices
    {
        public Task<string> CreateCategory(Category category);
        public Task<string> DeleteAsync(Category category);
        public Task<string> EditAsync(Category category);
        public Task<Category> GetByIdAsync(int id);
        public Task<Category> GetByIdWithIncludeAsync(int id);
        public Task<List<Category>> GetCategorytListAsync();
        public Task<List<Category>> GetByNameAsunc(string name);


    }
}
