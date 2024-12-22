using E_Commerce.Domain.Entities;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infastructure.Repositories
{
    public class CartRepo : GenaricRepo<ShoppingCart>, ICart
    {
        #region Fields
        private readonly DbSet<ShoppingCart> _userCarts;
        #endregion


        #region Constructor
        public CartRepo(ECommerceContext context) : base(context)
        {
            _userCarts = context.Set<ShoppingCart>();
        }
        #endregion
        #region Handle Function
        #endregion
    }
}
