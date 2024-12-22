using E_Commerce.Domain.Entities;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infastructure.Repositories
{
    public class ProductRepo : GenaricRepo<Product>, IProduct
    {
        #region Fields
        private readonly DbSet<Product> _userCarts;
        #endregion


        #region Constructor
        public ProductRepo(ECommerceContext context) : base(context)
        {
            _userCarts = context.Set<Product>();
        }
        #endregion
        #region Handle Function
        #endregion
    }
}
