using E_Commerce.Domain.Entities;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infastructure.Repositories
{
    public class CategoryRepo : GenaricRepo<Category>, ICategory
    {
        #region Fields
        private readonly DbSet<Category> _category;
        private readonly ECommerceContext _context;
        #endregion


        #region Constructor
        public CategoryRepo(ECommerceContext context) : base(context)
        {
            _category = context.Set<Category>();
            _context = context;
        }
        #endregion
        #region Handle Function


        public async Task<List<Category>> GetCategoryListAsync()
        {
            return await _category.Include(x => x.Products).ToListAsync();

        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            // Implement the check using AnyAsync
            return await _context.Categories.AnyAsync(c => c.CategoryId == categoryId);
        }
        #endregion
    }
}
