using E_Commerce.Application.Dtos;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Services.Abstraction.ShoppingCartService
{
    public interface IshoppingCartServices
    {

        Task<ShoppingCartDto> AddItemToCart(int userId, CartItemDto itemDto);
        Task UpdateItemAsync(int userId, int productId, int quantity);

        public IQueryable<Product> GetAllProductsQueryable();
        Task<string> RemoveItemFromCart(int userId, int productId , int Quantity);

       //   Task<IEnumerable<ShoppingCartItem>> GetCartItemsByUserId(int userId); // why 
        Task<decimal> TotalPayment(int userId);


         Task ClearCart(int userId);


    }
}
