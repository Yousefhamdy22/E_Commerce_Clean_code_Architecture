using DashBoard.Application.DTOs;
using DashBoard.Core.Entities;
using DashBoard.Infrastructure.Context;
using DashBoard.Infrastructure.Repositories;
using DashBoard.Infrastructure.Services.Abstractions;
using E_Commerce.Domain.Entities;
using E_Commerce.Infastructure.Data;
using E_Commerce.Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.Infrastructure.Services.Implementations
{
    public class ProductDashRepo<T> : GenericRepository<Product>, IproductDashRepo
    {
        #region Fields
        private readonly DbSet<DashProduct> _Products;
        #endregion


        #region Constructor
        public ProductDashRepo(DashBoardContext context) : base(context)
        {
            _Products = context.Set<DashProduct>();
        }

        #endregion


        #region Handle Function


     
        #endregion

    }
}
