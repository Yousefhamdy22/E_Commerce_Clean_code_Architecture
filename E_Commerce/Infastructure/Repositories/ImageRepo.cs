using E_Commerce.Domain.Entities;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infastructure.Repositories
{
    public class ImageRepo : GenaricRepo<ProductImages>, IImageRepo
    {
        #region Fields
        private readonly DbSet<ProductImages> _productImages;
        #endregion
        #region Constructor
        public ImageRepo(ECommerceContext context) : base(context)
        {
            _productImages = context.Set<ProductImages>();
        }
        #endregion

        #region Handle Function

        #endregion
    }
}
