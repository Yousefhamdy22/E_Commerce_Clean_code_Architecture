using E_Commerce.Domain.Entities;

namespace E_Commerce.Services.Abstraction.ShoppingCartItemService
{
    public interface IshoppigCartIremService //for handling quantity &  GetCartItem(showDetails)
    {
        IEnumerable<ShoppingCartItem> GetCartItems(int userId);

        Task<ShoppingCartItem> GetCartItem(int userId, int productId);
    }
}
