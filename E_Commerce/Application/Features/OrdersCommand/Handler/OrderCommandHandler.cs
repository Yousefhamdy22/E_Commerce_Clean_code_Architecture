using E_Commerce.Application.Base;
using E_Commerce.Application.Features.OrdersCommand.Models;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Services.Abstraction.OrderService;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Application.Features.OrdersCommand.Handler
{
    public class OrderCommandHandler : ResponseHandler,
      IRequestHandler<DeleteOrderCommand, Response<string>>
    {
        #region Fields
        private readonly IOrderService _orderServices;
        private readonly UserManager<User> _userManager; // UserManager to fetch user details
        #endregion

        #region Constructor
        public OrderCommandHandler(IOrderService orderServices, UserManager<User> userManager)
        {
            _orderServices = orderServices;
            _userManager = userManager; // Initialize the UserManager
        }
        #endregion

        #region HandleFunction
        public async Task<Response<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await _orderServices.CancelOrder(request.UserId, request.OrderNumber);
            if (result == "The order number is invalid.")
                return NotFound<string>(result);
            if (result == "Sorry, you cannot cancel the order after 3 days.")
                return BadRequest<string>(result);
            return Success<string>(result);
        }
        #endregion
    }
}
