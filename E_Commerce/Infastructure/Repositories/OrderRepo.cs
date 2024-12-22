using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerce.Infastructure.Repositories
{
    public class OrderRepo : GenaricRepo<Order>, IOrder
    {
        #region Fields
        private readonly DbSet<Order> _userCarts;
        #endregion


        #region Constructor
        public OrderRepo(ECommerceContext context) : base(context)
        {
            _userCarts = context.Set<Order>();
        }

        public Task Add(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(ICollection<OrderItem> entities)
        {
            throw new NotImplementedException();
        }

        public Task Delete(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(ICollection<OrderItem> entities)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> SingleOrDefaultAsync(Expression<Func<OrderItem, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task Update(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(ICollection<OrderItem> entities)
        {
            throw new NotImplementedException();
        }

        IQueryable<OrderItem> IRepository<OrderItem>.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<OrderItem> IRepository<OrderItem>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        IQueryable<OrderItem> IRepository<OrderItem>.GetTableAsTracking()
        {
            throw new NotImplementedException();
        }

        IQueryable<OrderItem> IRepository<OrderItem>.GetTableNoTracking()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Handle Function
        #endregion
    }
}
