using AutoMapper;
using E_Commerce.Application.Dtos;
using E_Commerce.Application.Features.Products.Command.Models;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Mapping.Products
{
    public partial class ProductsProfile : Profile
    {
        public void AddProductMapping()
        {

            CreateMap<AddProductCommand, Product>()
               .ForMember(dest => dest.Categoryid, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.solditems, opt => opt.MapFrom(src => src.solditems))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }


        
    }
}
