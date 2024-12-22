using E_Commerce.Domain.Entities;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infastructure.Repositories
{
    public class ReviewRepo : GenaricRepo<Review>, IReview
    {
        #region Fields
        private readonly DbSet<Review> _userCarts;
        #endregion


        #region Constructor
        public ReviewRepo(ECommerceContext context) : base(context)
        {
            _userCarts = context.Set<Review>();
        }
        #endregion
        #region Handle Function
        #endregion
    }
}
