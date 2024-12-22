using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace E_Commerce.Domain.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        Task<T> GetById(int id);
        IQueryable<T> GetAll(); 
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

        IQueryable<T> GetTableNoTracking();//no
        IQueryable<T> GetTableAsTracking();

        Task DeleteRangeAsync(ICollection<T> entities);

        IDbContextTransaction BeginTransaction();//no
        void Commit();//no
        void RollBack();//no
      
        Task AddRangeAsync(ICollection<T> entities);//no
        
        Task UpdateRangeAsync(ICollection<T> entities);//no
   
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);


    }
}
