using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Application.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using E_Commerce.Services.Abstraction.ShoppingCartService;
using E_Commerce.Infastructure.Data;
using E_Commerce.Infastructure.Abstraction;

namespace E_Commerce.Services.Implemantation.ShoppingCartServices
{
    public class ShoppingCartService : IshoppingCartServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProduct _prodictRepo;
        private readonly ILogger<ShoppingCartService> _logger;
        private readonly ECommerceContext _context;

        public ShoppingCartService(IUnitOfWork unitOfWork, ILogger<ShoppingCartService> logger, 
            IMapper mapper , ECommerceContext context, IProduct prodictRepo)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _prodictRepo = prodictRepo;
        }

        #region AddItemToCart

        public async Task<ShoppingCartDto> AddItemToCart(int userId, CartItemDto itemDto)
        {
            _logger.LogInformation("Adding product {ProductId} to the shopping cart for user" +
                " {UserId}", itemDto.ProductId, userId);

            try
            {
                var product = await _unitOfWork.ProductsRepo.GetTableNoTracking()
                                                .Where(x => x.UserId == itemDto.ProductId)
                                                .FirstOrDefaultAsync();


                var cart = await _unitOfWork.ShoppingCartsRepo.GetById(userId)
                         ?? new ShoppingCart { UserId = userId, CartItems = new List<ShoppingCartItem>() };

                if (cart.CartItems == null)
                {
                    cart.CartItems = new List<ShoppingCartItem>();
                }

                if (cart.ShoppingCartId == 0)
                {
                    // First, add the cart to the context to generate an Id
                    await _unitOfWork.ShoppingCartsRepo.Add(cart);

                }

                var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == itemDto.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity += itemDto.Quantity;
                   // existingItem.totalPrice = existingItem.Quantity * GetProductPrice(existingItem.ProductId);
                    existingItem.totalPrice = existingItem.Quantity * itemDto.Price;

                    _logger.LogInformation("Updated quantity of product {ProductId} to" +
                        " {Quantity} in user {UserId}'s cart", itemDto.ProductId, existingItem.Quantity, userId);
                }
                else
                {
                    var productPrice = GetProductPrice(itemDto.ProductId);

                    var newItem = new ShoppingCartItem
                    {

                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        Date = DateTime.Now,
                        totalPrice = productPrice * itemDto.Quantity,
                        //ShoppingCartItemId = itemDto.ShoppingCartItemId


                    };
                    cart.CartItems.Add(newItem);
                    _logger.LogInformation("Added product {ProductId} with quantity {Quantity} to user {UserId}'s cart"
                        , itemDto.ProductId, itemDto.Quantity, userId);
                }

                await _unitOfWork.Save();

                var cartDto = _mapper.Map<ShoppingCartDto>(cart);
                cartDto.TotalAmount = cart.CartItems.Sum(i => i.Quantity * GetProductPrice(i.ProductId));
               // cartDto.TotalAmount = cart.CartItems.Sum(i => i.Quantity * GetProductPrice(i.ProductId));

                return cartDto;
           
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding item {ProductId} to user {UserId}'s cart",
                    itemDto.ProductId, userId);
                throw;
            }
        }
        private decimal GetProductPrice(int productId )
        {
            // Retrieve the product from the database to get the price
            var product = _unitOfWork.ProductsRepo.GetById(productId);
            if (product == null)
            {
                throw new Exception($"Product with ID {productId} not found.");
            }

            return 50.00m;
        }


        //public async Task AddItemToCart(int userId, int productId, int quantity)
        //{
        //    _logger.LogInformation("Adding product {ProductId} to the shopping cart for user {UserId}", productId, userId);

        //    try
        //    {
        //        var cart = await _unitOfWork.ShoppingCartsRepo.GetById(userId);
        //        if (cart == null)
        //        {
        //            cart = new ShoppingCart { UserId = userId, CartItems = new List<ShoppingCartItem>() };
        //            _unitOfWork.ShoppingCartsRepo.Add(cart);
        //            _logger.LogInformation("Created new shopping cart for user {UserId}", userId);
        //        }

        //        var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
        //        if (existingItem != null)
        //        {
        //            existingItem.Quantity += quantity;
        //            _logger.LogInformation("Updated quantity of product {ProductId} to {Quantity} in user {UserId}'s cart"
        //                , productId, existingItem.Quantity, userId);
        //        }
        //        else
        //        {
        //            cart.CartItems.Add(new ShoppingCartItem
        //            {
        //                ShoppingCartId = cart.ShoppingCartId,
        //                ProductId = productId,
        //                Quantity = quantity
        //            });
        //            _logger.LogInformation("Added product {ProductId} with quantity {Quantity} to user {UserId}'s cart"
        //                , productId, quantity, userId);
        //        }

        //        await _unitOfWork.Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while adding item {ProductId} to user {UserId}'s cart", productId, userId);
        //        throw;
        //    }
        //}

        #endregion

        #region ClearCart

        public async Task ClearCart(int userId)
        {
            _logger.LogInformation("Clearing shopping cart for user {UserId}", userId);

            try
            {
                var cart = await _unitOfWork.ShoppingCartsRepo.GetById(userId);
                if (cart != null)
                {
                    cart.CartItems.Clear();
                    await _unitOfWork.Save();
                    _logger.LogInformation("Cleared shopping cart for user {UserId}", userId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while clearing cart for user {UserId}", userId);
                throw;
            }
        }
        #endregion

        #region RemoveItemFromCart
        public async Task<string> RemoveItemFromCart(int userId, int productId , int quantity)
        {
            _logger.LogInformation("Removing product {ProductId} from the shopping cart for user {UserId}"
                , productId, userId , quantity);
            try
            {
                var cart = await _unitOfWork.ShoppingCartsRepo.GetById(userId);


                if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
                {
                    throw new InvalidOperationException($"Shopping cart not found or empty for user {userId}");
                }

                var itemToRemove = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
                if (itemToRemove != null)
                {
                    cart.CartItems.Remove(itemToRemove);
                    await _unitOfWork.Save();
                    _logger.LogInformation("Removed product {ProductId} from user {UserId}'s cart",
                        productId, userId);
                }

                return "Product removed from cart.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing item {ProductId} from user {UserId}'s cart"
                    , productId, userId);
                throw;
            }
        }
        #endregion




        #region Update
        public async Task UpdateItemAsync(int userId, int productId, int quantity)
        {
            _logger.LogInformation("Updating product {ProductId} quantity to {Quantity} for user {UserId}"
                , productId, quantity, userId);

            try
            {
                var cart = await _unitOfWork.ShoppingCartsRepo.GetById(userId);
                if (cart == null) return;

                var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity = quantity;
                    await _unitOfWork.Save();
                    _logger.LogInformation("Updated product {ProductId} quantity to {Quantity} in user {UserId}'s cart"
                        , productId, quantity, userId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product {ProductId} quantity for user {UserId}"
                    , productId, userId);
                throw;
            }
        }
        #endregion


        #region GetCart
        public async Task<IEnumerable<ShoppingCart>> GetCard(int userId)
        {
            var cartItems = await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return cartItems;
        }
        public IQueryable<Product> GetAllProductsQueryable()
        {
            return _prodictRepo.GetTableNoTracking().Include(x => x.Images).AsQueryable();
        }
        public Task<decimal> TotalPayment(int userId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region TotalPayment
        //public async Task<decimal> TotalPayment(int userId)
        //{
        //    var cartItems = await _context.ShoppingCarts
        //        .Include(c => c.CartItems)
        //        .Where(c => c.UserId == userId)
        //        .ToListAsync();
        //    if (cartItems == null || !cartItems.Any())
        //    {
        //        return 0;
        //    }

        //    decimal totalPayment = cartItems.Sum(item => item.UserId * item.CartItems);
        //    return totalPayment; // Format to 2 decimal places
        //}
        #endregion












    }
}
