using E_Commerce.Application.Base;
using E_Commerce.Application.Dtos;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Command.Models
{
    public class AddCartCommand : IRequest<ShoppingCartDto>
    {
        public int UserId { get; set; }
        public CartItemDto CartItemDto { get; set; }

        public AddCartCommand(int userId, CartItemDto cartItemDto)
        {
            UserId = userId;
            CartItemDto = cartItemDto;

        }
    }
}
