using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Identity;

namespace E_Commerce.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepo { get; }
        IRepository<Product> ProductsRepo { get; }
        IRepository<Order> OrdersRepo { get; }
        IRepository<Category> CategoriesRepo { get; }
        IRepository<ShoppingCart> ShoppingCartsRepo { get; }
        IRepository<PaymentDetails> PaymentDetailsRepo { get; }
        IRepository<ShoppingCartItem> ShoppingCartItemRepo { get; }


        Task Save();
    }
}
