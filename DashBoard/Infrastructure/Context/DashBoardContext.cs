using DashBoard.Core.Entities;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.Infrastructure.Context
{
    public class DashBoardContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        public DashBoardContext(DbContextOptions options) : base(options)
        {

        }

        public DashBoardContext() { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
           

          
        //    base.OnModelCreating(modelBuilder);
        //}
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
       

    }
}
