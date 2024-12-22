using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Services.Abstraction.ICategoryServices;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services.Implemantation.CategoryService
{
    public class CategoryService : ICategoryServices
    {
        #region Fields
        private readonly ICategory _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructor
        public CategoryService(ICategory categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region HandleFunctions
        public async Task<string> CreateCategory(Category category)
        {
            // adding student 

            await _categoryRepository.Add(category);
            await _unitOfWork.Save();
            return "Added SuccessFully";
        }
        public async Task<string> EditAsync(Category category)
        {
            await _categoryRepository.Update(category);

            return "Success";

        }
        public async Task<string> DeleteAsync(Category category)
        {
            // do groub of tranactiom or never do any of it
            var Trans = _categoryRepository.BeginTransaction();
            try
            {
                // if (category.Products == null)
                //       context.Categories.Remove(category);
                //  else
                // {
                //    foreach (var product in category.Products)
                //    {
                //       context.products.Remove(product);
                //   }
                //   context.Categories.Remove(category);
                //   context.SaveChanges();
                //}
                await _categoryRepository.Delete(category);

                await _unitOfWork.Save();

                await Trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await Trans.RollbackAsync();
                return "Failed";
            }
        }
        public async Task<List<Category>> GetCategorytListAsync()
        {
            return await _categoryRepository.GetCategoryListAsync();
        }

        public async Task<Category> GetByIdWithIncludeAsync(int id)
        {
            var category = await _categoryRepository.GetTableNoTracking().Include(x => x.Products)
                .Where(x => x.CategoryId.Equals(id)).FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetTableNoTracking().Include(x => x.Products)
                .Where(x => x.CategoryId.Equals(id)).FirstOrDefaultAsync();
            return category;
        }

        public async Task<List<Category>> GetByNameAsunc(string name)
        {
            var categories = await _categoryRepository.GetTableNoTracking().Include(x => x.Products)
                .Where(x => x.Name.Equals(name)).ToListAsync();
            return categories;
        }

        #endregion
    }
}
