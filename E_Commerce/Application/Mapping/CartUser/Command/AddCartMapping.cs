using AutoMapper;
using E_Commerce.Application.Dtos;
using E_Commerce.Application.Features.Cart.Command.Models;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Mapping.CartUser.Command
{
    public partial class CartsProfile : Profile
    {
        public void AddCartMapping()
        {
            CreateMap<AddCartCommand, ShoppingCartItem>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.CartItemDto.Quantity))
               // .ForMember(dest => dest.ShoppingCartId , opt => opt.MapFrom(src => src.CartItemDto.))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.CartItemDto.ProductId));
        }
    }
}
