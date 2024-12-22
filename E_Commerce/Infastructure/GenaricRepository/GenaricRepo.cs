using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace E_Commerce.Infastructure.Repositories
{
    public class GenaricRepo<T> : IRepository<T> where T : class
    {

        private readonly ECommerceContext _context;

        public GenaricRepo(ECommerceContext context)
        {
            _context = context;
        }

        public  IQueryable<T> GetAll()
        {
            return  _context.Set<T>().AsQueryable(); 
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
        {
             await _context.Set<T>().AddAsync(entity);
        }

        public async Task Update(T entity)  
        {
             _context.Set<T>().Update(entity);
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _context.Set<T>().AsQueryable();

        }

        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();

        }

        public Task DeleteRangeAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public IDbContextTransaction BeginTransaction()
        {


            return _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();

        }

        public void RollBack()
        {
            _context.Database.RollbackTransaction();

        }


        public virtual async Task UpdateRangeAsync(ICollection<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }
    }
}
