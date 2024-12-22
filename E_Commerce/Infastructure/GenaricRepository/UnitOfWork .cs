using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Data;
using E_Commerce.Infastructure.Repositories;

namespace E_Commerce.Infastructure.GenaricRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceContext _context;
        private readonly ILogger<UnitOfWork> _logger;


        public IRepository<User> UserRepo { get; private set; }
        public IRepository<Product> ProductsRepo { get;  set; }
        public IRepository<Order> OrdersRepo { get; private set; }
        public IRepository<Category> CategoriesRepo { get; private set; }
        public IRepository<ShoppingCart> ShoppingCartsRepo { get; private set; }
        public IRepository<ShoppingCartItem> ShoppingCartItemRepo { get; private set; }
        public IRepository<PaymentDetails> PaymentDetailsRepo { get; private set; }

      
       

        public UnitOfWork(ECommerceContext context , ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;


            UserRepo = new GenaricRepo<User>(_context);
            ProductsRepo = new GenaricRepo<Product>(_context);
            OrdersRepo = new GenaricRepo<Order>(_context);
            CategoriesRepo = new GenaricRepo<Category>(_context);
            ShoppingCartsRepo = new GenaricRepo<ShoppingCart>(_context);
            ShoppingCartItemRepo = new GenaricRepo<ShoppingCartItem>(_context);
            PaymentDetailsRepo = new GenaricRepo<PaymentDetails>(_context);

      
        
        }

      

        // Dispose the context to free resources
        public void Dispose()
        {
            _context.Dispose();
        }

        #region SAVE
        public async Task Save()
        {
            _logger.LogInformation("Saving changes to the database");

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Changes saved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving changes to the database");
                throw;
            }
        }
        #endregion

    }
}
