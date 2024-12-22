using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace DashBoard.Infrastructure.Repositories
{
    public interface IGenaricRepository<T> where T : class
    {

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(int id);
        Task Save();

        Task<bool> DeleteAsync(int id);

        Task<T> GetById(int id);
        IQueryable<T> GetAll();

        IQueryable<T> GetTableNoTracking();//no
        IQueryable<T> GetTableAsTracking();

        Task DeleteRangeAsync(ICollection<T> entities);

        IDbContextTransaction BeginTransaction();//no
        void Commit();//no
        void RollBack();//no

        Task AddRangeAsync(ICollection<T> entities);//no

        Task UpdateRangeAsync(ICollection<T> entities);//no

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(int id);

        // Search and filter
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
        //  Task<IEnumerable<T>> SearchAsync(string searchTerm);

        // Pagination support
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize);



    }
}
