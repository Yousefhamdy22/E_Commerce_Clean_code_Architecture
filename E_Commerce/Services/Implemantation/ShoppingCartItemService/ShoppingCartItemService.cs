
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Services.Abstraction.ShoppingCartItemService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Services.Implemantation.ShoppingCartItemService
{
    public class ShoppingCartItemService : IshoppigCartIremService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ShoppingCartItemService> _logger;

        public ShoppingCartItemService(IUnitOfWork unitOfWork, ILogger<ShoppingCartItemService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #region GetCartItem_ProductForupdate_or_Checkout
        public async Task<ShoppingCartItem> GetCartItem(int userId, int productId)
        {
            try
            {
                var cartItem = await _unitOfWork.ShoppingCartItemRepo
                    .GetAll()
                    .Where(item => item.ShoppingCart.UserId == userId && item.ProductId == productId)
                    .FirstOrDefaultAsync();  // Also works now

                if (cartItem != null)
                {
                    _logger.LogInformation("Fetched cart item for user {UserId} and product {ProductId}", userId, productId);
                }
                else
                {
                    _logger.LogInformation("No cart item found for user {UserId} and product {ProductId}", userId, productId);
                }

                return cartItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching cart item for user {UserId} and product {ProductId}", userId, productId);
                throw;
            }
        }
        #endregion

        #region GetCartItems_As_History_or_See_His_Product_only
        public IEnumerable<ShoppingCartItem> GetCartItems(int userId)
        {
            try
            {
                var cartItems = _unitOfWork.ShoppingCartItemRepo
                    .GetAll()
                    .Where(item => item.ShoppingCart.UserId == userId)
                    .ToList();

                _logger.LogInformation("Fetched {Count} cart items for user {UserId}", cartItems.Count, userId);
                return cartItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching cart items for user {UserId}", userId);
                throw;
            }
        }
        #endregion

    }
}
