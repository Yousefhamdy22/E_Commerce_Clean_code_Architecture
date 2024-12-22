using AutoMapper;
using E_Commerce.Application.Features.Products.Command.Models;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Mapping.Products
{
    public partial class ProductsProfile : Profile
    {
        public void UpdateProduct()
        {

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Categoryid, opt => opt.MapFrom(src => src.Id)); 
            // Adjust this line based on your actual mapping logic
        }
    }
}
