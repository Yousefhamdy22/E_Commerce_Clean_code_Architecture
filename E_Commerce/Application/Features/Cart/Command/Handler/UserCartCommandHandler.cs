using AutoMapper;
using E_Commerce.Application.Base;
using E_Commerce.Application.Dtos;
using E_Commerce.Application.Features.Cart.Command.Models;
using E_Commerce.Services.Abstraction.ProductService;
using E_Commerce.Services.Abstraction.ShoppingCartService;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Command.Handler
{
    public class UserCartCommandHandler : ResponseHandler,
       IRequestHandler<AddCartCommand, ShoppingCartDto>,
        IRequestHandler<RemoveCartCommand , Response<string>>
              


    {
        #region Feilds
        private readonly IMapper _mapper;
        private readonly IshoppingCartServices _cartServices;
        private readonly IProductService _productServices;
        private readonly ILogger<AddCartCommand> _logger;
        #endregion

        #region Constructor
        public UserCartCommandHandler(IMapper mapper, IshoppingCartServices cartServices, 
                                                       IProductService productServices,
                                                       ILogger<AddCartCommand> logger )
        {
            _mapper = mapper;
            _cartServices = cartServices;
            _productServices = productServices;
            _logger = logger;
        }

        #endregion
        #region HandleFunction
        public async Task<ShoppingCartDto> Handle(AddCartCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AddCartCommand for user {UserId}", request.UserId);

            var cartDto = await _cartServices.AddItemToCart(request.UserId, request.CartItemDto);

            return cartDto;
        }

        public async Task<Response<string>> Handle(RemoveCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _cartServices.RemoveItemFromCart(request.ProductId, request.UserId, request.Quantity);

            if (result == "Product not found in cart.")
            {
                return NotFound<string>(result);
            }

            return Success<string>(result);
        }


        #endregion
    }
}
