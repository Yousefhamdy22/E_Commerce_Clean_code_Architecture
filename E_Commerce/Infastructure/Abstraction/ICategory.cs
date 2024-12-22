using E_Commerce.Application.Dtos;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Interfaces;

namespace E_Commerce.Infastructure.Abstraction
{
    public interface ICategory : IRepository<Category>
    {
        public Task<List<Category>> GetCategoryListAsync();
        public Task<bool> CategoryExists(int categoryId);


    }
}
